using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Bdaya.Abp.TemporalEntities.EntityFrameworkCore;

[ConnectionStringName(TemporalEntitiesDbProperties.ConnectionStringName)]
public interface ITemporalEntitiesDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
