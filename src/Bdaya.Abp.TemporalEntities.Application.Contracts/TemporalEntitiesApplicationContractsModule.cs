using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(TemporalEntitiesDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class TemporalEntitiesApplicationContractsModule : AbpModule
{

}
