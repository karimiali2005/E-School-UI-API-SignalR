using Es.DataLayerCore.DTOs.Versioning;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IVersioningService
    {
        Task<VersioningViewModel> GetLoadVersion();
    }
}
