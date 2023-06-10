using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Bdaya.Abp.TemporalEntities.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(AbpValidationModule)
)]
public class TemporalEntitiesDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TemporalEntitiesDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TemporalEntitiesResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/TemporalEntities");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("TemporalEntities", typeof(TemporalEntitiesResource));
        });
    }
}
