using Bdaya.Abp.TemporalEntities.Entities;
using Bdaya.Abp.TemporalEntities.MongoDB.Entities;
using Bdaya.Abp.TemporalEntities.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

[DependsOn(typeof(TemporalEntitiesTestBaseModule), typeof(TemporalEntitiesMongoDbModule))]
public class TemporalEntitiesMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = MongoDbFixture.ConnectionString.Split('?');
        var connectionString =
            stringArray[0].EnsureEndsWith('/')
            + "Db_"
            + Guid.NewGuid().ToString("N")
            + "/?"
            + stringArray[1];

        context.Services.RegisterTemporalEntityHandler<
            ProductModel,
            Guid,
            ProductModelHistory,
            TemporalProductModelHandler
        >();
        context.Services.RegisterTemporalEntityHandler<JobModel, Guid>();
        context.Services.RegisterTemporalEntityHandler<Order, Guid>();

        context.Services.AddMongoDbContext<TestMongoDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}
