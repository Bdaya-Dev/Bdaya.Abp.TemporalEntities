using Localization.Resources.AbpUi;
using Bdaya.Abp.TemporalEntities.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(TemporalEntitiesApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class TemporalEntitiesHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TemporalEntitiesHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TemporalEntitiesResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
