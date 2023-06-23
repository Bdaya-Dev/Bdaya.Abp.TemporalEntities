using Bdaya.Abp.TemporalEntities.Entities;
using Bdaya.Abp.TemporalEntities.MongoDB.Entities;
using Bdaya.Abp.TemporalEntities.Repositories;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.MongoDB;
using Xunit;

namespace Bdaya.Abp.TemporalEntities;

public class HistoryEntity_Tests : TemporalEntitiesDomainTestBase
{
    //private readonly SampleManager _sampleManager;
    private readonly IRepository<Order, Guid> _orderRepository;
    private readonly IRepository<ProductModel, Guid> _productRepository;
    private readonly IRepository<ProductModelHistory, Guid> _productHistoryRepository;
    private readonly IRepository<JobModel, Guid> _jobsRepo;
    private readonly IRepository<EntityHistoryAggregateRoot<JobModel, Guid>, Guid> _jobsHistoryRepo;

    public HistoryEntity_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
        _orderRepository = GetRequiredService<IRepository<Order, Guid>>();
        _productRepository = GetRequiredService<
            ITemporalRepository<ProductModel, ProductModelHistory, Guid>
        >();
        _productHistoryRepository = GetRequiredService<IRepository<ProductModelHistory, Guid>>();
        _jobsRepo = GetRequiredService<ITemporalRepository<JobModel, Guid>>();
        _jobsHistoryRepo = GetRequiredService<
            IRepository<EntityHistoryAggregateRoot<JobModel, Guid>, Guid>
        >();
    }

    [Fact]
    public Task DependenciesAreOfCorrectType()
    {
        //Tests getting the dependencies

        return Task.CompletedTask;
    }
}
