using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Bdaya.Abp.TemporalEntities;

public static class HistoryEntityHelper
{
    public static IQueryable<T> WhereEntityIsValidAt<T>(this IQueryable<T> query, DateTime date)
        where T : class, IEntityHistory
    {
        return query.Where(x => x.ValidFrom <= date && x.ValidTo > date);
    }

    public static Task<THistory> GetEntityAt<THistory, TEntity, TKey>(
        this IRepository<THistory> repo,
        TKey entityId,
        DateTime? date,
        Expression<Func<THistory, bool>>? predicate = null
    )
        where THistory : class, IEntity, IEntityHistory<TEntity>
        where TEntity : class, IEntity<TKey>
    {
        Expression<Func<THistory, bool>> finalExpr =
            date == null
                ? x => x.Entity.Id!.Equals(entityId) && x.ValidTo == DateTime.MaxValue
                : x => x.Entity.Id!.Equals(entityId) && x.ValidFrom <= date && x.ValidTo > date;
        if (predicate != null)
        {
            finalExpr = finalExpr.And(predicate);
        }
        return repo.FindAsync(finalExpr);
    }

    public static Task<List<THistory>> GetListAt<THistory>(
        this IRepository<THistory> repo,
        DateTime? date,
        Expression<Func<THistory, bool>>? predicate = null
    )
        where THistory : class, IEntity, IEntityHistory
    {
        Expression<Func<THistory, bool>> finalPredicate =
            date == null
                ? x => x.ValidTo == DateTime.MaxValue
                : x => x.ValidFrom <= date && x.ValidTo > date;
        if (predicate != null)
        {
            finalPredicate = finalPredicate.And(predicate);
        }
        return repo.GetListAsync(finalPredicate);
    }

    public static Task<List<THistory>> GetListAt<THistory>(
        this IRepository<THistory> repo,
        HashSet<DateTime?> dates,
        Expression<Func<THistory, bool>>? predicate = null
    )
        where THistory : class, IEntity, IEntityHistory
    {
        var smallPredicates = dates.Select(
            date =>
                date == null
                    ? (x => x.ValidTo == DateTime.MaxValue)
                    : (Expression<Func<THistory, bool>>)(
                        x => x.ValidFrom <= date && x.ValidTo > date
                    )
        );
        //a - a1,a2,a3,a4
        //b - b1,b2,b3,b4

        Expression<Func<THistory, bool>>? finalPredicate;
        if (smallPredicates.Any())
        {
            finalPredicate = smallPredicates.Aggregate((a, b) => a.Or(b));
        }
        else
        {
            finalPredicate = (z) => true;
        }

        if (predicate != null)
        {
            finalPredicate = finalPredicate.And(predicate);
        }
        return repo.GetListAsync(finalPredicate);
    }
}
