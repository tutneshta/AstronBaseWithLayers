using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Fiscal;
using AstronBase.Domain.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Service.Interfaces
{
    public interface IModelService
    {
        Task<IBaseResponse<IEnumerable<Model>>> GetModels();

        Task<IBaseResponse<Model>> GetModel(int id);

        Task<IBaseResponse<Model>> GetModelByName(string name);

        Task<IBaseResponse<bool>> DeleteModel(int id);

        Task<IBaseResponse<ModelCreateViewModel>> CreateModel(ModelCreateViewModel model);

        Task<IBaseResponse<Model>> Edit(int id, ModelEditViewModel model);

        Task<IBaseResponse<IEnumerable<Model>>> GetModelBySearch(string search);
    }
}
