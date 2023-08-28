using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstronBase.Domain.Entity;
using AstronBase.Domain.ViewModels.StatusFiscal;

namespace AstronBase.Service.Interfaces
{
    public interface IStatusFiscalService
    {
        Task<IBaseResponse<IEnumerable<StatusFiscal>>> GetStatusFiscals();

        Task<IBaseResponse<StatusFiscal>> GetStatusFiscal(int id);

        Task<IBaseResponse<StatusFiscal>> GetStatusFiscalByName(string name);

        Task<IBaseResponse<bool>> DeleteStatusFiscal(int id);

        Task<IBaseResponse<StatusFiscalCreateViewModel>> CreateStatusFiscal(StatusFiscalCreateViewModel model);

        Task<IBaseResponse<StatusFiscal>> Edit(int id, StatusFiscalEditViewModel model);

        Task<IBaseResponse<IEnumerable<StatusFiscal>>> GetStatusFiscalBySearch(string search);
    }
}
