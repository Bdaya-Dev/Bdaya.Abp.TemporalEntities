using System;
using System.Collections.Generic;
using System.Text;

namespace Bdaya.Abp.TemporalEntities;

public interface IEntityHistory
{
    DateTime ValidFrom { get; set; }
    DateTime ValidTo { get; set; }
}
