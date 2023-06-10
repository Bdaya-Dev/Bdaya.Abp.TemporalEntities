using Bdaya.Abp.TemporalEntities.Localization;
using Volo.Abp.Application.Services;

namespace Bdaya.Abp.TemporalEntities;

public abstract class TemporalEntitiesAppService : ApplicationService
{
    protected TemporalEntitiesAppService()
    {
        LocalizationResource = typeof(TemporalEntitiesResource);
        ObjectMapperContext = typeof(TemporalEntitiesApplicationModule);
    }
}
