using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Bdaya.Abp.TemporalEntities.EntityFrameworkCore;

[ConnectionStringName(TemporalEntitiesDbProperties.ConnectionStringName)]
public class TemporalEntitiesDbContext : AbpDbContext<TemporalEntitiesDbContext>, ITemporalEntitiesDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public TemporalEntitiesDbContext(DbContextOptions<TemporalEntitiesDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureTemporalEntities();
    }
}
