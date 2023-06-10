using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Bdaya.Abp.TemporalEntities;
public class EntityHistoryAggregateRoot<T> : AggregateRoot<Guid>, IEntityHistory<T>
    where T : class, IEntity<Guid>
{
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
