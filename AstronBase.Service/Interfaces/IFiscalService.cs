using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstronBase.Domain.ViewModels.Fiscal;

namespace AstronBase.Service.Interfaces
{
    public interface IFiscalService
    {
        Task<IBaseResponse<IEnumerable<Fiscal>>> GetFiscals();

        Task<IBaseResponse<Fiscal>> GetFiscal(int id);

        Task<IBaseResponse<Fiscal>> GetFiscalBySerial(string serial);

        Task<IBaseResponse<bool>> DeleteFiscal(int id);

        Task<IBaseResponse<FiscalCreateViewModel>> CreateFiscal(FiscalCreateViewModel model);

        Task<IBaseResponse<Fiscal>> Edit(int id, FiscalEditViewModel model);

        Task<IBaseResponse<IEnumerable<Fiscal>>> GetFiscalBySearch(string search);
    }
}
