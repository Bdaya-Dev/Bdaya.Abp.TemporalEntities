using Bdaya.Abp.TemporalEntities.Entities;
using MongoDB.Driver;
using System;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB.Entities;

public class TestMongoDbContext : AbpMongoDbContext, ITemporalEntitiesMongoDbContext
{
    public IMongoCollection<Order> Orders => Collection<Order>();

    public IMongoCollection<ProductModel> Products => Collection<ProductModel>();
    public IMongoCollection<ProductModelHistory> ProductHistory => Collection<ProductModelHistory>();

    public IMongoCollection<JobModel> Jobs => Collection<JobModel>();
    public IMongoCollection<EntityHistoryAggregateRoot<JobModel, Guid>> JobHistory => Collection<EntityHistoryAggregateRoot<JobModel, Guid>>();
    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

    }
}
