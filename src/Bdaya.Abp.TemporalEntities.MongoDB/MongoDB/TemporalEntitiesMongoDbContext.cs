using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

[ConnectionStringName(TemporalEntitiesDbProperties.ConnectionStringName)]
public class TemporalEntitiesMongoDbContext : AbpMongoDbContext, ITemporalEntitiesMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureTemporalEntities();
    }
}
