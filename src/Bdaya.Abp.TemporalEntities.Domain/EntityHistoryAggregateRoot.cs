using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Bdaya.Abp.TemporalEntities;

public class EntityHistoryAggregateRoot<T, TKey> : AggregateRoot<Guid>, IEntityHistory<T>
    where T : class, IEntity<TKey>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected EntityHistoryAggregateRoot() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public EntityHistoryAggregateRoot(T entity, DateTime validFrom, DateTime validTo)
    {
        Entity = entity;
        ValidFrom = validFrom;
        ValidTo = validTo;
    }

    public T Entity { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}
