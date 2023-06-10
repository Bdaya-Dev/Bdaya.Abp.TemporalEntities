using Bdaya.Abp.TemporalEntities.Entities;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Bdaya.Abp.TemporalEntities;

public class HistoryEntity_Tests : TemporalEntitiesDomainTestBase
{
    //private readonly SampleManager _sampleManager;
    private readonly IRepository<Order, Guid> _orderRepository;
    private readonly IRepository<Product, Guid> _productRepository;
    public HistoryEntity_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
        _orderRepository = GetRequiredService<IRepository<Order, Guid>>();
        _productRepository = GetRequiredService<IRepository<Product, Guid>>();
    }

    [Fact]
    public Task Method1Async()
    {
        return Task.CompletedTask;
    }
}
