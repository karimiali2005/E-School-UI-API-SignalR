using Es.DataLayerCore.DTOs.Discipline;
using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IDisciplineService
    {
        Task<List<DisciplineShowResult>> DisciplineShow(int userId);
        Task<List<DisciplineType>> DisciplineTypeShow();
        Task<bool> DisciplineInsert(Discipline discipline);
        Task<bool> StudentDisciplineDelete(List<int> StudentDisciplineIds);
    }
}
