using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Service.Interfaces
{
    public interface IEngineerService
    {
        Task<IBaseResponse<IEnumerable<Engineer>>> GetEngineers();

        Task<IBaseResponse<Engineer>> GetEngineer(int id);

        Task<IBaseResponse<Engineer>> GetEngineerByName(string name);

        Task<IBaseResponse<bool>> DeleteEngineer(int id);

        Task<IBaseResponse<EngineerCreateViewModel>> CreateEngineer(EngineerCreateViewModel model);

        Task<IBaseResponse<Engineer>> Edit(int id, EngineerEditViewModel model);

        Task<IBaseResponse<IEnumerable<Engineer>>> GetEngineerBySearch(string search);
    }
}
