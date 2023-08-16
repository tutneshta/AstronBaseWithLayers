using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstronBase.DAL.Interfaces;
using AstronBase.DAL.Repositories;
using AstronBase.Domain.Entity;
using AstronBase.Domain.Enum;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Model;
using AstronBase.Domain.ViewModels.StatusBlank;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class StatusBlankService : IStatusBlankService
    {
        private readonly IStatusBlankRepository _statusBlankRepository;

        public StatusBlankService(IStatusBlankRepository statusBlankRepository)
        {
            _statusBlankRepository = statusBlankRepository;
        }

        public async Task<IBaseResponse<IEnumerable<StatusBlank>>> GetStatusBlanks()
        {
            var baseResponse = new BaseResponse<IEnumerable<StatusBlank>>();

            try
            {
                var statusBlank = await _statusBlankRepository.Select();

                if (statusBlank.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = statusBlank;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<StatusBlank>>()
                {
                    Description = $"[GetStatusBlanks] : {e.Message}",
                };
            }
        }

        public async Task<IBaseResponse<StatusBlank>> GetStatusBlank(int id)
        {
            var baseResponse = new BaseResponse<StatusBlank>();

            try
            {
                var statusBlank = await _statusBlankRepository.Get(id);

                if (statusBlank == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = statusBlank;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusBlank>()
                {
                    Description = $"[GetStatusBlank] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<StatusBlank>> GetStatusBlankByName(string name)
        {
            var baseResponse = new BaseResponse<StatusBlank>();

            try
            {
                var statusBlank = await _statusBlankRepository.GetByName(name);

                if (statusBlank == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = statusBlank;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusBlank>()
                {
                    Description = $"[GetStatusBlankByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteStatusBlank(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var statusBlank = await _statusBlankRepository.Get(id);

                if (statusBlank == null)
                {
                    baseResponse.Description = "StatusBlank not found";
                    baseResponse.StatusCode = StatusCode.StatusBlankNotFound;
                    return baseResponse;
                }

                await _statusBlankRepository.Delete(statusBlank);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteStatusBlank] : {e.Message}",
                    StatusCode = StatusCode.StatusBlankNotFound
                };
            }
        }

        public async Task<IBaseResponse<StatusBlankCreateViewModel>> CreateStatusBlank(StatusBlankCreateViewModel model)
        {
            var baseResponse = new BaseResponse<StatusBlankCreateViewModel>();

            try
            {
                var statusBlank = new StatusBlank()
                {

                    Id = model.Id,
                    Name = model.Name,

                };

                await _statusBlankRepository.Create(statusBlank);
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusBlankCreateViewModel>()
                {
                    Description = $"[CreateStatusBlank] : {e.Message}",
                    StatusCode = StatusCode.StatusBlankNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<StatusBlank>> Edit(int id, StatusBlankEditViewModel model)
        {
            var baseResponse = new BaseResponse<StatusBlank>();

            try
            {
                var statusBlank = await _statusBlankRepository.Get(id);

                if (statusBlank == null)
                {
                    baseResponse.Description = "StatusBlank not found";
                    baseResponse.StatusCode = StatusCode.StatusBlankNotFound;
                    return baseResponse;
                }

                model.Name = model.Name;
                model.Id = model.Id;

                await _statusBlankRepository.Update(statusBlank);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusBlank>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.StatusBlankNotFound
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<StatusBlank>>> GetStatusBlankBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<StatusBlank>>();
            try
            {
                var statusBlank = await _statusBlankRepository.GetBySearch(search);
                if (statusBlank == null)
                {
                    baseResponse.Description = "StatusBlank not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = statusBlank;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<StatusBlank>>()
                {
                    Description = $"[GetStatusBlankBySearch] : {e.Message}",

                };
            }
        }
    }
}
