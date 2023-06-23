using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

public static class HistoryEntityHelper
{
    public static async Task<T> GetHistoryEntityAt<T>(
        this IRepository<T> repository,
        DateTime date
    )
        where T : class, IEntityHistory, IEntity
    {
        var q = await repository.GetMongoQueryableAsync();
        return await q.WhereEntityIsValidAt(date).FirstOrDefaultAsync();
    }

    public static async Task<TEntity> GetEntityAt<TEntity, TEntityHistory>(
        this IRepository<TEntityHistory> repository,
        DateTime date
    )
        where TEntity : class, IEntity
        where TEntityHistory : class, IEntityHistory<TEntity>, IEntity
    {
        var q = await repository.GetMongoQueryableAsync();
        return (await q.WhereEntityIsValidAt(date).FirstOrDefaultAsync()).Entity;
    }

    public static IMongoQueryable<T> WhereEntityIsValidAt<T>(
        this IMongoQueryable<T> query,
        DateTime date
    )
        where T : class, IEntityHistory
    {
        return (IMongoQueryable<T>)(query as IQueryable<T>).WhereEntityIsValidAt(date);
    }
}
