using System;
using System.Collections.Generic;
using System.Text;

namespace Bdaya.Abp.TemporalEntities;

public interface IHasEntityHistory { }

public interface IHasEntityHistory<T> : IHasEntityHistory
    where T : class, IEntityHistory { }

public interface IEntityHistory
{
    DateTime ValidFrom { get; set; }
    DateTime ValidTo { get; set; }
}
