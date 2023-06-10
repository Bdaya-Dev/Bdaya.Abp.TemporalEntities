using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

public class TemporalMongoDbRepositoryBase<TMongoDbContext, TEntity>
    : MongoDbRepository<TMongoDbContext, TEntity>
    where TMongoDbContext : IAbpMongoDbContext
    where TEntity : class, IEntity
{
    public TemporalMongoDbRepositoryBase(IMongoDbContextProvider<TMongoDbContext> dbContextProvider)
        : base(dbContextProvider) { }
}
