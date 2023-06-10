using Bdaya.Abp.TemporalEntities.Entities;
using MongoDB.Driver;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB.Entities;

public class TestMongoDbContext : AbpMongoDbContext, ITemporalEntitiesMongoDbContext
{
    public IMongoCollection<Order> Orders => Collection<Order>();
    public IMongoCollection<Product> Products => Collection<Product>();
    public IMongoCollection<ProductHistory> ProductHistory => Collection<ProductHistory>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

    }
}
