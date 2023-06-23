using System;
using Volo.Abp.Domain.Entities;

namespace Bdaya.Abp.TemporalEntities.Entities;

public class JobModel : AggregateRoot<Guid>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected JobModel() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public JobModel(Guid id, string title)
        : base(id)
    {
        Title = title;
    }

    public string Title { get; set; }    
}
//Products
//Apple -> ID: 1
//Orange -> ID: 2

//ProductHistory
//(1) Apple -> ID: 1, ValidFrom: 1/1/2021, ValidTo: 1/1/2022
//(2) Apple -> ID: 1, ValidFrom: 1/1/2022, ValidTo: 31/12/9999
