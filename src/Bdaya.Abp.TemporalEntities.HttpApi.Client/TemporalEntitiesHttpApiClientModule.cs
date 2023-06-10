using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(TemporalEntitiesApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TemporalEntitiesHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TemporalEntitiesApplicationContractsModule).Assembly,
            TemporalEntitiesRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TemporalEntitiesHttpApiClientModule>();
        });

    }
}
