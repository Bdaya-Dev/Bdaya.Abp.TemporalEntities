using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Bdaya.Abp.TemporalEntities.EntityFrameworkCore;

[DependsOn(
    typeof(TemporalEntitiesDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class TemporalEntitiesEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TemporalEntitiesDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
