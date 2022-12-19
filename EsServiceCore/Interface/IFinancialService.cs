using Es.DataLayerCore.DTOs.Finacial;
using Es.DataLayerCore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsServiceCore.Interface
{
    public interface IFinancialService
    {
        Task<List<FinancialShowResult>> FinancialShow(int userId);
        Task<List<FinancialSendType>> FinancialSendTypeShow();
        Task<bool> FinancialInsert(Financial financial);
        Task<bool> StudentFinancialDelete(List<int> StudentFinancialIds);
    }
}
