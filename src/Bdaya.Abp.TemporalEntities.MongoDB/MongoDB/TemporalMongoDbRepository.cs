using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Timing;
using System.Linq;
using SharpCompress.Common;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.DependencyInjection;
using Bdaya.Abp.TemporalEntities.Repositories;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

public class TemporalMongoDbRepository<TMongoDbContext, TEntity, TKey>
    : TemporalMongoDbRepository<
        TMongoDbContext,
        TEntity,
        EntityHistoryAggregateRoot<TEntity, TKey>,
        TKey
    >,
        ITemporalRepository<TEntity, TKey>
    where TMongoDbContext : IAbpMongoDbContext
    where TEntity : class, IEntity<TKey>
{
    protected TemporalMongoDbRepository(
        IMongoDbContextProvider<TMongoDbContext> dbContextProvider,
        IRepository<EntityHistoryAggregateRoot<TEntity, TKey>, Guid> historyRepository
    )
        : base(dbContextProvider, historyRepository) { }

    protected override EntityHistoryAggregateRoot<TEntity, TKey> CreateHistoryEntity(
        TEntity entity,
        DateTime ValidFrom,
        DateTime ValidTo
    )
    {
        return new EntityHistoryAggregateRoot<TEntity, TKey>(entity, ValidFrom, ValidTo);
    }
}

public abstract class TemporalMongoDbRepository<TMongoDbContext, TEntity, TEntityHistory, TKey>
    : MongoDbRepository<TMongoDbContext, TEntity, TKey>,
        ITemporalRepository<TEntity, TEntityHistory, TKey>
    where TMongoDbContext : IAbpMongoDbContext
    where TEntity : class, IEntity<TKey>
    where TEntityHistory : class, IEntityHistory<TEntity>, IEntity<Guid>
{
    public IClock Clock => LazyServiceProvider.LazyGetRequiredService<IClock>();

    public TemporalMongoDbRepository(
        IMongoDbContextProvider<TMongoDbContext> dbContextProvider,
        IRepository<TEntityHistory, Guid> historyRepository
    )
        : base(dbContextProvider)
    {
        HistoryRepository = historyRepository;
    }

    protected IRepository<TEntityHistory, Guid> HistoryRepository { get; }

    protected abstract TEntityHistory CreateHistoryEntity(
        TEntity entity,
        DateTime ValidFrom,
        DateTime ValidTo
    );

    private async Task HandleDeleteById(TKey id, CancellationToken cancellationToken = default)
    {
        var date = Clock.Now;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var oldHistory = await HistoryRepository.SingleOrDefaultAsync(
            x => x.Entity.Id.Equals(id) && x.ValidTo == DateTime.MaxValue,
            cancellationToken: cancellationToken
        );
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        if (oldHistory != null)
        {
            oldHistory.ValidTo = date;
            await HistoryRepository.UpdateAsync(oldHistory, cancellationToken: cancellationToken);
        }
    }

    private async Task HandleManyDeleteById(
        HashSet<TKey> ids,
        CancellationToken cancellationToken = default
    )
    {
        var date = Clock.Now;

        //get old history entity, change ValidTo to date
        var oldHistory = await HistoryRepository.GetListAsync(
            x => ids.Contains(x.Entity.Id) && x.ValidTo == DateTime.MaxValue
        );
        foreach (var item in oldHistory)
        {
            item.ValidTo = date;
        }
        if (oldHistory.Any())
        {
            await HistoryRepository.UpdateManyAsync(
                oldHistory,
                cancellationToken: cancellationToken
            );
        }
    }

    public override async Task DeleteAsync(
        TEntity entity,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        await base.DeleteAsync(entity, autoSave, cancellationToken);
        await HandleDeleteById(entity.Id, cancellationToken);
    }

    public override async Task DeleteManyAsync(
        IEnumerable<TEntity> entities,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        await base.DeleteManyAsync(entities, autoSave, cancellationToken);
        var ids = new HashSet<TKey>(entities.Select(x => x.Id));
        await HandleManyDeleteById(ids, cancellationToken);
    }

    public override async Task DeleteManyAsync(
        IEnumerable<TKey> ids,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        var idsSet = new HashSet<TKey>(ids);
        await base.DeleteManyAsync(idsSet, autoSave, cancellationToken);
        await HandleManyDeleteById(idsSet, cancellationToken);
    }

    public override async Task DeleteAsync(
        TKey id,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        await base.DeleteAsync(id, autoSave, cancellationToken);
        await HandleDeleteById(id, cancellationToken);
    }

    public override async Task<TEntity> InsertAsync(
        TEntity entity,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        var result = await base.InsertAsync(entity, autoSave, cancellationToken);
        var date = Clock.Now;
        await HistoryRepository.InsertAsync(
            CreateHistoryEntity(result, date, DateTime.MaxValue),
            cancellationToken: cancellationToken
        );
        return result;
    }

    public override async Task InsertManyAsync(
        IEnumerable<TEntity> entities,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        await base.InsertManyAsync(entities, autoSave, cancellationToken);
        var date = Clock.Now;
        await HistoryRepository.InsertManyAsync(
            entities.Select(x => CreateHistoryEntity(x, date, DateTime.MaxValue)),
            cancellationToken: cancellationToken
        );
    }

    public override async Task<TEntity> UpdateAsync(
        TEntity entity,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        var result = await base.UpdateAsync(entity, autoSave, cancellationToken);
        var date = Clock.Now;
        //get old history entity, change ValidTo to date
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var oldHistory = await HistoryRepository.SingleOrDefaultAsync(
            x => x.Entity.Id.Equals(result.Id) && x.ValidTo == DateTime.MaxValue,
            cancellationToken: cancellationToken
        );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        if (oldHistory != null)
        {
            oldHistory.ValidTo = date;
            await HistoryRepository.UpdateAsync(oldHistory, cancellationToken: cancellationToken);
        }

        await HistoryRepository.InsertAsync(
            CreateHistoryEntity(result, date, DateTime.MaxValue),
            cancellationToken: cancellationToken
        );
        return result;
    }

    public override async Task UpdateManyAsync(
        IEnumerable<TEntity> entities,
        bool autoSave = false,
        CancellationToken cancellationToken = default
    )
    {
        await base.UpdateManyAsync(entities, autoSave, cancellationToken);
        var date = Clock.Now;
        var ids = new HashSet<TKey>(entities.Select(x => x.Id));
        //get old history entity, change ValidTo to date
        var oldHistory = await HistoryRepository.GetListAsync(
            x => ids.Contains(x.Entity.Id) && x.ValidTo == DateTime.MaxValue
        );
        foreach (var item in oldHistory)
        {
            item.ValidTo = date;
        }
        if (oldHistory.Any())
        {
            await HistoryRepository.UpdateManyAsync(
                oldHistory,
                cancellationToken: cancellationToken
            );
        }
        await HistoryRepository.InsertManyAsync(
            entities.Select(x => CreateHistoryEntity(x, date, DateTime.MaxValue)),
            cancellationToken: cancellationToken
        );
    }
}
