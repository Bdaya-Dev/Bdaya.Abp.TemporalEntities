using Bdaya.Abp.TemporalEntities.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Bdaya.Abp.TemporalEntities;

public abstract class TemporalEntitiesController : AbpControllerBase
{
    protected TemporalEntitiesController()
    {
        LocalizationResource = typeof(TemporalEntitiesResource);
    }
}
