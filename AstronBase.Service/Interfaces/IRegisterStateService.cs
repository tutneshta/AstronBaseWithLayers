using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstronBase.Domain.Entity;
using AstronBase.Domain.ViewModels.RegisterState;

namespace AstronBase.Service.Interfaces
{
    public interface IRegisterStateService
    {
        Task<IBaseResponse<IEnumerable<RegisterState>>> GetRegisterStates();

        Task<IBaseResponse<RegisterState>> GetRegisterState(int id);

        Task<IBaseResponse<RegisterState>> GetRegisterStateByName(string name);

        Task<IBaseResponse<bool>> DeleteRegisterState(int id);

        Task<IBaseResponse<RegisterStateCreateViewModel>> CreateRegisterState(RegisterStateCreateViewModel model);

        Task<IBaseResponse<RegisterState>> Edit(int id, RegisterStateEditViewModel model);

        Task<IBaseResponse<IEnumerable<RegisterState>>> GetRegisterStateBySearch(string search);
    }
}
