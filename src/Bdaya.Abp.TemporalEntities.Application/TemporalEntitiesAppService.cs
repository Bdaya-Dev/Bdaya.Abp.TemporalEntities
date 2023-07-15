using Volo.Abp.Application.Services;

namespace Bdaya.Abp.TemporalEntities;

public abstract class TemporalEntitiesAppService : ApplicationService
{
    protected TemporalEntitiesAppService()
    {
        ObjectMapperContext = typeof(TemporalEntitiesApplicationModule);
    }
}
