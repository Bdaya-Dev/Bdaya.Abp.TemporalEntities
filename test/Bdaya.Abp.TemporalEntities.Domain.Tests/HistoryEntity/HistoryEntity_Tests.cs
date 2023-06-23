using Bdaya.Abp.TemporalEntities.Entities;
using Bdaya.Abp.TemporalEntities.MongoDB.Entities;
using Bdaya.Abp.TemporalEntities.Repositories;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Timing;
using Xunit;

namespace Bdaya.Abp.TemporalEntities;

public class HistoryEntity_Tests : TemporalEntitiesDomainTestBase
{
    //private readonly SampleManager _sampleManager;
    private readonly IRepository<Order, Guid> _orderRepository;
    private readonly IRepository<EntityHistoryAggregateRoot<Order, Guid>, Guid> _orderHistoryRepo;
    private readonly IRepository<ProductModel, Guid> _productRepository;
    private readonly IRepository<ProductModelHistory, Guid> _productHistoryRepository;
    private readonly IRepository<JobModel, Guid> _jobsRepo;
    private readonly IRepository<EntityHistoryAggregateRoot<JobModel, Guid>, Guid> _jobsHistoryRepo;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IClock _clock;
    private readonly IDataFilter _dataFilter;


    public HistoryEntity_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _orderRepository = GetRequiredService<IRepository<Order, Guid>>();
        _orderHistoryRepo = GetRequiredService<
            IRepository<EntityHistoryAggregateRoot<Order, Guid>, Guid>
        >();
        _productRepository = GetRequiredService<IRepository<ProductModel, Guid>>();
        _productHistoryRepository = GetRequiredService<IRepository<ProductModelHistory, Guid>>();
        _jobsRepo = GetRequiredService<IRepository<JobModel, Guid>>();
        _jobsHistoryRepo = GetRequiredService<
            IRepository<EntityHistoryAggregateRoot<JobModel, Guid>, Guid>
        >();
        _clock = GetRequiredService<IClock>();
    }

    [Fact]
    public Task DependenciesAreOfCorrectType()
    {
        //Tests getting the dependencies

        return Task.CompletedTask;
    }

    [Fact]
    public async Task TestCreateTemporalTable()
    {
        var res = await _productRepository.GetListAsync();
        var resHistory = await _productHistoryRepository.GetListAsync();
        res.Count.ShouldBe(0);
        resHistory.Count.ShouldBe(0);

        var inserted = await _productRepository.InsertAsync(
            new ProductModel(_guidGenerator.Create(), "P1")
        );
        res = await _productRepository.GetListAsync();
        resHistory = await _productHistoryRepository.GetListAsync();
        res.Count.ShouldBe(1);
        resHistory.Count.ShouldBe(1);
        resHistory[0].Entity.Id.ShouldBe(inserted.Id);
        resHistory[0].Entity.Name.ShouldBe(inserted.Name);
        resHistory[0].ValidTo.ShouldBe(DateTime.MaxValue);
    }

    [Fact]
    public async Task TestUpdateTemporalTable()
    {
        var inserted = await _productRepository.InsertAsync(
            new ProductModel(_guidGenerator.Create(), "P1")
        );
        inserted.Name = "P2";
        inserted = await _productRepository.UpdateAsync(inserted);
        var res = await _productRepository.GetListAsync();
        var resHistory = (await _productHistoryRepository.GetListAsync())
            .OrderBy(x => x.ValidFrom)
            .ToList();
        res.Count.ShouldBe(1);
        resHistory.Count.ShouldBe(2);
        resHistory[0].Entity.Id.ShouldBe(inserted.Id);
        resHistory[0].Entity.Name.ShouldBe("P1");
        resHistory[0].ValidTo.ShouldNotBe(DateTime.MaxValue);
        resHistory[1].Entity.Id.ShouldBe(inserted.Id);
        resHistory[1].Entity.Name.ShouldBe("P2");
        resHistory[1].ValidTo.ShouldBe(DateTime.MaxValue);
    }

    [Fact]
    public async Task TestDeleteTemporalTable()
    {
        var inserted = await _productRepository.InsertAsync(
            new ProductModel(_guidGenerator.Create(), "P1")
        );
        await _productRepository.DeleteAsync(inserted);
        var res = await _productRepository.GetListAsync();
        var resHistory = (await _productHistoryRepository.GetListAsync())
            .OrderBy(x => x.ValidFrom)
            .ToList();
        res.Count.ShouldBe(0);
        resHistory.Count.ShouldBe(1);
        resHistory[0].Entity.Id.ShouldBe(inserted.Id);
        resHistory[0].Entity.Name.ShouldBe("P1");
        resHistory[0].ValidTo.ShouldNotBe(DateTime.MaxValue);
    }

    [Fact]
    public async Task TestGetRelevantTemporalTable()
    {
        var inserted = await _productRepository.InsertAsync(
            new ProductModel(_guidGenerator.Create(), "P1")
        );
        var now = _clock.Now;

        await Task.Delay(2000);
        inserted.Name = "P2";
        inserted = await _productRepository.UpdateAsync(inserted);
        var testPoint1 = now + TimeSpan.FromMilliseconds(500);
        var res = await _productHistoryRepository.GetListAt(testPoint1);
        var resSingle = await _productHistoryRepository.GetEntityAt<
            ProductModelHistory,
            ProductModel,
            Guid
        >(inserted.Id, testPoint1);
        resSingle.ShouldNotBeNull();
        res.Count.ShouldBe(1);
        resSingle.Entity.Id.ShouldBe(inserted.Id);
        resSingle.Entity.Name.ShouldBe("P1");
        res[0].Entity.Id.ShouldBe(inserted.Id);
        res[0].Entity.Name.ShouldBe("P1");
        res = await _productHistoryRepository.GetListAt(now + TimeSpan.FromSeconds(7));
        res[0].Entity.Id.ShouldBe(inserted.Id);
        res[0].Entity.Name.ShouldBe("P2");
    }
}
