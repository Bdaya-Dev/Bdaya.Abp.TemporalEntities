using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Bdaya.Abp.TemporalEntities.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
