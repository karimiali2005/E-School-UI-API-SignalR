using Es.DataLayerCore.DTOs.Statist;
using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IPreRegistrationService
    {
        Task<List<PreRegistration>> PreRegistrationShow();
        Task<PreRegistration> PreRegistrationByID(int preRegistrationID);
        Task<bool> PreRegistrationArchive(int preRegistrationID);
        Task<StatisticsShowResult> StatisticsShow();
    }
}
