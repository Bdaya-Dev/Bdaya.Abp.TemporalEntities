using System;
using Volo.Abp.Domain.Entities;

namespace Bdaya.Abp.TemporalEntities;

public interface IEntityHistory<T> : IEntityHistory
        where T : class, IEntity
{
    public T Entity { get; set; }
}
