using System;
using Volo.Abp.Domain.Entities;

namespace Bdaya.Abp.TemporalEntities.Entities;

public class OrderItem : Entity<Guid>
{
    public Guid ProductId { get; set; }
}
