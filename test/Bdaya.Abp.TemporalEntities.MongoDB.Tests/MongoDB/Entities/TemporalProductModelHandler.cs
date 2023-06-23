using Bdaya.Abp.TemporalEntities.Entities;
using Bdaya.Abp.TemporalEntities.Repositories;
using System;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;

namespace Bdaya.Abp.TemporalEntities.MongoDB.Entities;

public class TemporalProductModelHandler
    : TemporalEntityHandler<ProductModel, Guid, ProductModelHistory>
{
    public TemporalProductModelHandler(IRepository<ProductModelHistory, Guid> historyRepository)
        : base(historyRepository) { }

    protected override ProductModelHistory CreateHistoryEntity(
        EntityChangedEventData<ProductModel> eventData,
        DateTime ValidFrom,
        DateTime ValidTo
    )
    {
        return new ProductModelHistory(eventData.Entity, ValidFrom, ValidTo)
        {
            CreatorId = eventData switch
            {
                EntityCreatedEventData<ProductModel> created => created.Entity.CreatorId,
                EntityUpdatedEventData<ProductModel> updated => updated.Entity.LastModifierId,
                _ => throw new NotImplementedException(),
            }
        };
    }
}
