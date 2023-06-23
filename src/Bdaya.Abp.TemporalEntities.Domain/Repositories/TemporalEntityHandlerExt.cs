namespace Bdaya.Abp.TemporalEntities.Repositories;

using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

public static class TemporalEntityHandlerExt
{
    public static IServiceCollection RegisterTemporalEntityHandler<TEntity, TKey>(
        this IServiceCollection s
    )
        where TEntity : class, IEntity<TKey>
    {
        s.AddTransient<
            ILocalEventHandler<EntityChangedEventData<TEntity>>,
            TemporalEntityHandler<TEntity, TKey>
        >();
        return s;
    }

    public static IServiceCollection RegisterTemporalEntityHandler<
        TEntity,
        TKey,
        THistory,
        THandler
    >(this IServiceCollection s)
        where TEntity : class, IEntity<TKey>
        where THistory : class, IEntityHistory<TEntity>, IEntity<Guid>
        where THandler : TemporalEntityHandler<TEntity, TKey, THistory>
    {
        s.AddTransient<THandler>();
        s.AddTransient<ILocalEventHandler<EntityChangedEventData<TEntity>>, THandler>();
        return s;
    }
}
