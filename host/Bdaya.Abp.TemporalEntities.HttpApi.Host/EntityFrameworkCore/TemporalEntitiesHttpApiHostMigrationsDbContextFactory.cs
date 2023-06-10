using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Bdaya.Abp.TemporalEntities.EntityFrameworkCore;

public class TemporalEntitiesHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<TemporalEntitiesHttpApiHostMigrationsDbContext>
{
    public TemporalEntitiesHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TemporalEntitiesHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("TemporalEntities"));

        return new TemporalEntitiesHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
