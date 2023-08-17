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
using AstronBase.Domain.ViewModels.StatusFiscal;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class StatusFiscalService : IStatusFiscalService
    {
        private readonly IStatusFiscalRepository _statusFiscalRepository;

        public StatusFiscalService(IStatusFiscalRepository statusFiscalRepository)
        {
            _statusFiscalRepository = statusFiscalRepository;
        }


        public async Task<IBaseResponse<IEnumerable<StatusFiscal>>> GetStatusFiscals()
        {
            var baseResponse = new BaseResponse<IEnumerable<StatusFiscal>>();

            try
            {
                var statusFiscals = await _statusFiscalRepository.Select();

                if (statusFiscals.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = statusFiscals;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<StatusFiscal>>()
                {
                    Description = $"[GetStatusFiscals] : {e.Message}",
                };
            }
        }

        public async Task<IBaseResponse<StatusFiscal>> GetStatusFiscal(int id)
        {
            var baseResponse = new BaseResponse<StatusFiscal>();

            try
            {
                var statusFiscal = await _statusFiscalRepository.Get(id);

                if (statusFiscal == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = statusFiscal;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusFiscal>()
                {
                    Description = $"[GetStatusFiscal] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<StatusFiscal>> GetStatusFiscalByName(string name)
        {
            var baseResponse = new BaseResponse<StatusFiscal>();

            try
            {
                var statusFiscal = await _statusFiscalRepository.GetByName(name);

                if (statusFiscal == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = statusFiscal;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusFiscal>()
                {
                    Description = $"[GetStatusFiscalByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteStatusFiscal(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var statusFiscal = await _statusFiscalRepository.Get(id);

                if (statusFiscal == null)
                {
                    baseResponse.Description = "StatusFiscal not found";
                    baseResponse.StatusCode = StatusCode.StatusFiscalNotFound;
                    return baseResponse;
                }

                await _statusFiscalRepository.Delete(statusFiscal);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteStatusFiscal] : {e.Message}",
                    StatusCode = StatusCode.StatusFiscalNotFound
                };
            }
        }

        public async Task<IBaseResponse<StatusFiscalCreateViewModel>> CreateStatusFiscal(StatusFiscalCreateViewModel model)
        {
            var baseResponse = new BaseResponse<StatusFiscalCreateViewModel>();

            try
            {
                var statusFiscal = new StatusFiscal()
                {

                    Id = model.Id,
                    Name = model.Name,

                };

                await _statusFiscalRepository.Create(statusFiscal);
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusFiscalCreateViewModel>()
                {
                    Description = $"[CreateStatusFiscal] : {e.Message}",
                    StatusCode = StatusCode.StatusFiscalNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<StatusFiscal>> Edit(int id, StatusFiscalEditViewModel model)
        {
            var baseResponse = new BaseResponse<StatusFiscal>();

            try
            {
                var statusFiscal = await _statusFiscalRepository.Get(id);

                if (statusFiscal == null)
                {
                    baseResponse.Description = "StatusFiscal not found";
                    baseResponse.StatusCode = StatusCode.StatusFiscalNotFound;
                    return baseResponse;
                }

                model.Name = model.Name;
                model.Id = model.Id;

                await _statusFiscalRepository.Update(statusFiscal);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<StatusFiscal>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.StatusFiscalNotFound
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<StatusFiscal>>> GetStatusFiscalBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<StatusFiscal>>();
            try
            {
                var statusFiscal = await _statusFiscalRepository.GetBySearch(search);
                if (statusFiscal == null)
                {
                    baseResponse.Description = "StatusFiscal not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = statusFiscal;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<StatusFiscal>>()
                {
                    Description = $"[GetStatusFiscalBySearch] : {e.Message}",

                };
            }
        }
    }
}
