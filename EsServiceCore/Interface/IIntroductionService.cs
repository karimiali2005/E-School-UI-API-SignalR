using Es.DataLayerCore.DTOs.Introduction;
using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IIntroductionService
    {
        Task<List<IntroductionShowAllResult>> IntroductionShowAll();
        Task<List<IntroductionType>> IntroductionTypeShowAll();
        Task<bool> IntroductionInsert(Introduction introduction);
        Task<Introduction> IntroductionGetById(int introductionId);
        Task<bool> IntroductionUpdate(Introduction introduction);
        Task<bool> IntroductionDelete(int id);
    }
}
