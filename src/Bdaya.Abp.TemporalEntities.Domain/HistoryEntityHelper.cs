using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bdaya.Abp.TemporalEntities;

public static class HistoryEntityHelper
{
    public static IQueryable<T> WhereEntityIsValidAt<T>(this IQueryable<T> query, DateTime date)
        where T : class, IEntityHistory
    {
        return query.Where(x => x.ValidFrom <= date && x.ValidTo > date);
    }
}
