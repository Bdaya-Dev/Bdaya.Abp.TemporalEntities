using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

[DependsOn(
    typeof(TemporalEntitiesDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class TemporalEntitiesMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<TemporalEntitiesMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
