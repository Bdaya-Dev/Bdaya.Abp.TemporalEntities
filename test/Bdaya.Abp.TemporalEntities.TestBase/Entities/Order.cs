using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bdaya.Abp.TemporalEntities.Entities;

public class Order : CreationAuditedAggregateRoot<Guid>
{
    protected Order() { }

    public Order(Guid id)
        : base(id) { }

    public List<OrderItem> Items { get; set; }
}
