using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bdaya.Abp.TemporalEntities.Entities;

public class ProductModel : AggregateRoot<Guid>, ISoftDelete
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ProductModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ProductModel(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }

    public bool IsDeleted { get; set; }
}
//Products
//Apple -> ID: 1
//Orange -> ID: 2

//ProductHistory
//(1) Apple -> ID: 1, ValidFrom: 1/1/2021, ValidTo: 1/1/2022
//(2) Apple -> ID: 1, ValidFrom: 1/1/2022, ValidTo: 31/12/9999
