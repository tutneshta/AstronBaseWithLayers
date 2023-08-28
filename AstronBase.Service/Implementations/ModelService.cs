using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AstronBase.DAL.Interfaces;
using AstronBase.DAL.Repositories;
using AstronBase.Domain.Entity;
using AstronBase.Domain.Enum;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Model;
using AstronBase.Domain.ViewModels.Store;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Model>>> GetModels()
        {

            var baseResponse = new BaseResponse<IEnumerable<Model>>();

            try
            {
                var models = await _modelRepository.Select();

                if (models.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = models;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Model>>()
                {
                    Description = $"[GetModels] : {e.Message}",
                };
            }
        }

        public async Task<IBaseResponse<Model>> GetModel(int id)
        {
            var baseResponse = new BaseResponse<Model>();

            try
            {
                var model = await _modelRepository.Get(id);

                if (model == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = model;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Model>()
                {
                    Description = $"[GetModel] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Model>> GetModelByName(string name)
        {
            var baseResponse = new BaseResponse<Model>();

            try
            {
                var model = await _modelRepository.GetByName(name);

                if (model == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = model;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Model>()
                {
                    Description = $"[GetModelByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteModel(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var model = await _modelRepository.Get(id);

                if (model == null)
                {
                    baseResponse.Description = "Model not found";
                    baseResponse.StatusCode = StatusCode.ModelNotFound;
                    return baseResponse;
                }

                await _modelRepository.Delete(model);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteModel] : {e.Message}",
                    StatusCode = StatusCode.ModelNotFound
                };
            }
        }

        public async Task<IBaseResponse<ModelCreateViewModel>> CreateModel(ModelCreateViewModel model1)
        {
            var baseResponse = new BaseResponse<ModelCreateViewModel>();

            try
            {
                var model = new Model()
                {

                 Id = model1.Id,
                 Name = model1.Name,

                };

                await _modelRepository.Create(model);
            }
            catch (Exception e)
            {
                return new BaseResponse<ModelCreateViewModel>()
                {
                    Description = $"[CreateModel] : {e.Message}",
                    StatusCode = StatusCode.ModelNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Model>> Edit(int id, ModelEditViewModel model1)
        {
            var baseResponse = new BaseResponse<Model>();

            try
            {
                var model = await _modelRepository.Get(id);

                if (model == null)
                {
                    baseResponse.Description = "Model not found";
                    baseResponse.StatusCode = StatusCode.ModelNotFound;
                    return baseResponse;
                }

                model.Name = model.Name;
                model.Id = model.Id;

                await _modelRepository.Update(model);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Model>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.ModelNotFound
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Model>>> GetModelBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<Model>>();
            try
            {
                var model = await _modelRepository.GetBySearch(search);
                if (model == null)
                {
                    baseResponse.Description = "Model not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = model;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Model>>()
                {
                    Description = $"[GetModelBySearch] : {e.Message}",

                };
            }
        }
    }
}
