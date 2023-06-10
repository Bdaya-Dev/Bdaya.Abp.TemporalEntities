//using Bdaya.Abp.TemporalEntities.EntityFrameworkCore;
using Bdaya.Abp.TemporalEntities.MongoDB;
using Volo.Abp.Modularity;

namespace Bdaya.Abp.TemporalEntities;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    //typeof(TemporalEntitiesEntityFrameworkCoreTestModule)
    typeof(TemporalEntitiesMongoDbTestModule)
    )]
public class TemporalEntitiesDomainTestModule : AbpModule
{

}
