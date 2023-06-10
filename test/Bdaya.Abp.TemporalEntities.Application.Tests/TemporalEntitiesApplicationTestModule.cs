using Volo.Abp.Modularity;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(TemporalEntitiesApplicationModule),
    typeof(TemporalEntitiesDomainTestModule)
    )]
public class TemporalEntitiesApplicationTestModule : AbpModule
{

}
