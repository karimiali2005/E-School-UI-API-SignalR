using Es.DataLayerCore.DTOs.Ceremony;
using Es.DataLayerCore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface ICeremonyService
    {
        Task<List<CeremonyShowResult>> CeremonyShow();
        Task<List<CeremonyType>> CeremonyTypeShow();
        Task<bool> CeremonyInsert(Ceremony ceremony);
        Task<Ceremony> CeremonyGetById(int ceremonyId);
        Task<bool> CeremonyDelete(int id);
    }
}
