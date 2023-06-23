using System;
using Volo.Abp.Auditing;

namespace Bdaya.Abp.TemporalEntities.Entities;

public class ProductModelHistory : EntityHistoryAggregateRoot<ProductModel, Guid>, IMayHaveCreator
{
    protected ProductModelHistory() { }

    public ProductModelHistory(ProductModel entity, DateTime validFrom, DateTime validTo)
        : base(entity, validFrom, validTo) { }

    public Guid? CreatorId { get; set; }
}
//Products
//Apple -> ID: 1
//Orange -> ID: 2

//ProductHistory
//(1) Apple -> ID: 1, ValidFrom: 1/1/2021, ValidTo: 1/1/2022
//(2) Apple -> ID: 1, ValidFrom: 1/1/2022, ValidTo: 31/12/9999
