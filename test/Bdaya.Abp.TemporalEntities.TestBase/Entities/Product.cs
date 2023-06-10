using System;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bdaya.Abp.TemporalEntities.Entities;

public class Product : AggregateRoot<Guid>, IHasEntityHistory<ProductHistory>, ISoftDelete
{
    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}

public class ProductHistory : EntityHistoryAggregateRoot<Product>, IMayHaveCreator
{
    public ProductHistory(Product entity, DateTime validFrom, DateTime validTo)
        : base(entity, validFrom, validTo) { }

    public Guid? CreatorId { get; set; }
}
//Products
//Apple -> ID: 1
//Orange -> ID: 2

//ProductHistory
//(1) Apple -> ID: 1, ValidFrom: 1/1/2021, ValidTo: 1/1/2022
//(2) Apple -> ID: 1, ValidFrom: 1/1/2022, ValidTo: 31/12/9999
