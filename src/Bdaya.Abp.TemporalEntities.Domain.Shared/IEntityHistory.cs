using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bdaya.Abp.TemporalEntities;

public interface IEntityHistory
{
    DateTime ValidFrom { get; set; }
    DateTime ValidTo { get; set; }
}

public static class IEntityHistoryExt
{
    public static T? PickValidFor<T>(this IEnumerable<T>? entities, DateTime? date)
        where T : class, IEntityHistory
    {
        if (entities == null)
        {
            return null;
        }
        if (date == null)
        {
            return entities.FirstOrDefault(x => x.ValidTo == DateTime.MaxValue);
        }
        return entities.FirstOrDefault(x => x.ValidFrom <= date && x.ValidTo > date);
    }
}
