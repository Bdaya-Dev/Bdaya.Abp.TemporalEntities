using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

public static class TemporalEntitiesMongoDbContextExtensions
{
    public static void ConfigureTemporalEntities(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
