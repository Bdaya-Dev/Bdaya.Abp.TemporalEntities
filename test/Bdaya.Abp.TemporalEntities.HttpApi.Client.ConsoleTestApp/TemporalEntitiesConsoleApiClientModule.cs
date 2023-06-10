using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TemporalEntitiesHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class TemporalEntitiesConsoleApiClientModule : AbpModule
{

}
