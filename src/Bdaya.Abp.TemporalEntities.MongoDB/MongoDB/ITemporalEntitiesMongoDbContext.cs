using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

[ConnectionStringName(TemporalEntitiesDbProperties.ConnectionStringName)]
public interface ITemporalEntitiesMongoDbContext : IAbpMongoDbContext { }
