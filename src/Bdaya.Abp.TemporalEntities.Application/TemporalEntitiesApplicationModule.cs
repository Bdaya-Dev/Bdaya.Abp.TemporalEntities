using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(TemporalEntitiesDomainModule),
    typeof(TemporalEntitiesApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class TemporalEntitiesApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<TemporalEntitiesApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TemporalEntitiesApplicationModule>(validate: true);
        });
    }
}
