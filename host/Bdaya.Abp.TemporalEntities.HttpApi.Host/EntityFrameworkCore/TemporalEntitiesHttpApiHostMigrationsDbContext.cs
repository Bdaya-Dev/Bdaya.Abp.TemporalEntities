using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Bdaya.Abp.TemporalEntities.EntityFrameworkCore;

public class TemporalEntitiesHttpApiHostMigrationsDbContext : AbpDbContext<TemporalEntitiesHttpApiHostMigrationsDbContext>
{
    public TemporalEntitiesHttpApiHostMigrationsDbContext(DbContextOptions<TemporalEntitiesHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureTemporalEntities();
    }
}
