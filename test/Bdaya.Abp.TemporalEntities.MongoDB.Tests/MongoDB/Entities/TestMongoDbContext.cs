using Bdaya.Abp.TemporalEntities.Entities;
using MongoDB.Driver;
using System;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB.Entities;

public class TestMongoDbContext : AbpMongoDbContext
{
    public IMongoCollection<Order> Orders => Collection<Order>();
    [MongoCollection("OrderHistory")]
    public IMongoCollection<EntityHistoryAggregateRoot<Order, Guid>> OrderHistory =>
      Collection<EntityHistoryAggregateRoot<Order, Guid>>();

    public IMongoCollection<ProductModel> Products => Collection<ProductModel>();
    public IMongoCollection<ProductModelHistory> ProductHistory =>
        Collection<ProductModelHistory>();

    public IMongoCollection<JobModel> Jobs => Collection<JobModel>();

    [MongoCollection("JobHistory")]
    public IMongoCollection<EntityHistoryAggregateRoot<JobModel, Guid>> JobHistory =>
        Collection<EntityHistoryAggregateRoot<JobModel, Guid>>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);
    }
}
