namespace Bdaya.Abp.TemporalEntities.Repositories;

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus;
using Volo.Abp.Timing;

public class TemporalEntityHandler<T, TKey>
    : TemporalEntityHandler<T, TKey, EntityHistoryAggregateRoot<T, TKey>>
    where T : class, IEntity<TKey>
{
    public TemporalEntityHandler(
        IRepository<EntityHistoryAggregateRoot<T, TKey>, Guid> historyRepository
    )
        : base(historyRepository) { }

    protected override EntityHistoryAggregateRoot<T, TKey> CreateHistoryEntity(
        EntityChangedEventData<T> eventData,
        DateTime ValidFrom,
        DateTime ValidTo
    )
    {
        return new EntityHistoryAggregateRoot<T, TKey>(eventData.Entity, ValidFrom, ValidTo);
    }
}

public abstract class TemporalEntityHandler<TEntity, TKey, THistory>
    : ILocalEventHandler<EntityChangedEventData<TEntity>>
    where TEntity : class, IEntity<TKey>
    where THistory : class, IEntityHistory<TEntity>, IEntity<Guid>
{
    protected IRepository<THistory, Guid> HistoryRepository { get; }
    public IAbpLazyServiceProvider? LazyServiceProvider { get; set; }
    protected IClock Clock => LazyServiceProvider!.LazyGetRequiredService<IClock>();

    public TemporalEntityHandler(IRepository<THistory, Guid> historyRepository)
    {
        HistoryRepository = historyRepository;
    }

    protected abstract THistory CreateHistoryEntity(
        EntityChangedEventData<TEntity> eventData,
        DateTime ValidFrom,
        DateTime ValidTo
    );

    public async Task HandleEventAsync(EntityChangedEventData<TEntity> eventData)
    {
        var date = Clock.Now;
        switch (eventData)
        {
            case EntityDeletedEventData<TEntity> deleted:
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var oldHistory = await HistoryRepository.SingleOrDefaultAsync(
                    x => x.Entity.Id.Equals(deleted.Entity.Id) && x.ValidTo == DateTime.MaxValue
                );
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (oldHistory != null)
                {
                    oldHistory.ValidTo = date;
                    await HistoryRepository.UpdateAsync(oldHistory);
                }
                break;
            }
            case EntityUpdatedEventData<TEntity> updated:
            {
                //get old history entity, change ValidTo to date
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var oldHistory = await HistoryRepository.SingleOrDefaultAsync(
                    x => x.Entity.Id.Equals(updated.Entity.Id) && x.ValidTo == DateTime.MaxValue
                );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                if (oldHistory != null)
                {
                    oldHistory.ValidTo = date;
                    await HistoryRepository.UpdateAsync(oldHistory);
                }

                await HistoryRepository.InsertAsync(
                    CreateHistoryEntity(updated, date, DateTime.MaxValue)
                );
                break;
            }
            case EntityCreatedEventData<TEntity> created:
                await HistoryRepository.InsertAsync(
                    CreateHistoryEntity(created, date, DateTime.MaxValue)
                );
                break;

            default:
                break;
        }
    }
}
