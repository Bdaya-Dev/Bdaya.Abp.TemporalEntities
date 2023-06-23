namespace Bdaya.Abp.TemporalEntities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

public interface ITemporalRepository<TEntity, TKey>
    : ITemporalRepository<TEntity, EntityHistoryAggregateRoot<TEntity, TKey>, TKey>
    where TEntity : class, IEntity<TKey> { }

public interface ITemporalRepository<TEntity, TEntityHistory, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TEntityHistory : class, IEntityHistory<TEntity>, IEntity<Guid> { }
