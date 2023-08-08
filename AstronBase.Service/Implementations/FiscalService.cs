using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AstronBase.DAL.Interfaces;
using AstronBase.DAL.Repositories;
using AstronBase.Domain.Entity;
using AstronBase.Domain.Enum;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.Fiscal;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class FiscalService : IFiscalService
    {
        private readonly IFiscalRepository _fiscalRepository;

        public FiscalService(IFiscalRepository fiscalRepository)
        {
            _fiscalRepository = fiscalRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Fiscal>>> GetFiscals()
        {
            var baseResponse = new BaseResponse<IEnumerable<Fiscal>>();

            try
            {
                var fiscals = await _fiscalRepository.Select();

                if (fiscals.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = fiscals;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Fiscal>>()
                {
                    Description = $"[GetFiscals] : {e.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Fiscal>> GetFiscal(int id)
        {
            var baseResponse = new BaseResponse<Fiscal>();

            try
            {
                var fiscal = await _fiscalRepository.Get(id);

                if (fiscal == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = fiscal;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Fiscal>()
                {
                    Description = $"[GetFiscal] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Fiscal>> GetFiscalBySerial(string serial)
        {
            var baseResponse = new BaseResponse<Fiscal>();

            try
            {
                var fiscal = await _fiscalRepository.GetByserial(serial);

                if (fiscal == null)
                {
                    baseResponse.Description = "Fiscal not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = fiscal;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Fiscal>()
                {
                    Description = $"[GetFiscalBySerial] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteFiscal(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var fiscal = await _fiscalRepository.Get(id);

                if (fiscal == null)
                {
                    baseResponse.Description = "Fiscal not found";
                    baseResponse.StatusCode = StatusCode.FiscalNotFound;
                    return baseResponse;
                }

                await _fiscalRepository.Delete(fiscal);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFiscal] : {e.Message}",
                    StatusCode = StatusCode.FiscalNotFound
                };
            }
        }

        public async Task<IBaseResponse<FiscalCreateViewModel>> CreateFiscal(FiscalCreateViewModel model)
        {

            var baseResponse = new BaseResponse<FiscalCreateViewModel>();

            try
            {
                var fiscal = new Fiscal()
                {
                    SerialNumber = model.SerialNumber,
                    CompanyId = model.CompanyId,
                    StoreId = model.StoreId,
                    RegisterStateId = model.RegisterStateId,
                    ModelId = model.ModelId,
                    StatusFiscalId = model.StatusFiscalId,
                    EngineerId = model.EngineerId,
                    Note = model.Note,
                };

                await _fiscalRepository.Create(fiscal);
            }
            catch (Exception e)
            {
                return new BaseResponse<FiscalCreateViewModel>()
                {
                    Description = $"[CreateFiscal] : {e.Message}",
                    StatusCode = StatusCode.FiscalNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Fiscal>> Edit(int id, FiscalEditViewModel model)
        {
            var baseResponse = new BaseResponse<Fiscal>();
            var fiscal = await _fiscalRepository.Get(id);

            try
            {
                if (fiscal == null)
                {
                    baseResponse.Description = "Fiscal not found";
                    baseResponse.StatusCode = StatusCode.FiscalNotFound;
                    return baseResponse;
                }


                fiscal.SerialNumber = model.SerialNumber;
                fiscal.CompanyId = model.CompanyId;
                fiscal.StoreId = model.StoreId;
                fiscal.RegisterStateId = model.RegisterStateId;
                fiscal.ModelId = model.ModelId;
                fiscal.StatusFiscalId = model.StatusFiscalId;
                fiscal.EngineerId = model.EngineerId;
                fiscal.Note = model.Note;

                await _fiscalRepository.Update(fiscal);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Fiscal>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.FiscalNotFound
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Fiscal>>> GetFiscalBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<Fiscal>>();
            try
            {
                var fiscal = await _fiscalRepository.GetBySearch(search);
                if (fiscal == null)
                {
                    baseResponse.Description = "Fiscal not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = fiscal;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Fiscal>>()
                {
                    Description = $"[GetFiscalBySearch] : {e.Message}",

                };
            }
        }
    }
}
