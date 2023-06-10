using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Bdaya.Abp.TemporalEntities.Samples;

public class SampleAppService : TemporalEntitiesAppService, ISampleAppService
{
    public Task<SampleDto> GetAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }

    [Authorize]
    public Task<SampleDto> GetAuthorizedAsync()
    {
        return Task.FromResult(
            new SampleDto
            {
                Value = 42
            }
        );
    }
}
