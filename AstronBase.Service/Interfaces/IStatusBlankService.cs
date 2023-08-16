using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.StatusBlank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Service.Interfaces
{
    public interface IStatusBlankService
    {
        Task<IBaseResponse<IEnumerable<StatusBlank>>> GetStatusBlanks();

        Task<IBaseResponse<StatusBlank>> GetStatusBlank(int id);

        Task<IBaseResponse<StatusBlank>> GetStatusBlankByName(string name);

        Task<IBaseResponse<bool>> DeleteStatusBlank(int id);

        Task<IBaseResponse<StatusBlankCreateViewModel>> CreateStatusBlank(StatusBlankCreateViewModel model);

        Task<IBaseResponse<StatusBlank>> Edit(int id, StatusBlankEditViewModel model);

        Task<IBaseResponse<IEnumerable<StatusBlank>>> GetStatusBlankBySearch(string search);
    }
}
