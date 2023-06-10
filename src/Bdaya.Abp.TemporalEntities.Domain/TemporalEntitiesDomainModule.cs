using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TemporalEntitiesDomainSharedModule)
)]
public class TemporalEntitiesDomainModule : AbpModule
{

}
