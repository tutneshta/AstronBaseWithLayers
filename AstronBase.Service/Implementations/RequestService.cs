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
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.Request;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Request>>> GetRequests()
        {
            var baseResponse = new BaseResponse<IEnumerable<Request>>();

            try
            {
                var requests = await _requestRepository.Select();

                if (requests.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = requests;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Request>>()
                {
                    Description = $"[GetRequests] : {e.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Request>> GetRequest(int id)
        {
            var baseResponse = new BaseResponse<Request>();

            try
            {
                var request = await _requestRepository.Get(id);

                if (request == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = request;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Request>()
                {
                    Description = $"[GetClient] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }




        public async Task<IBaseResponse<IEnumerable<Request>>> GetRequestBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<Request>>();
            try
            {
                var requests = await _requestRepository.GetBySearch(search);
                if (requests == null)
                {
                    baseResponse.Description = "Request not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = requests;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Request>>()
                {
                    Description = $"[GetRequestBySearch] : {e.Message}",

                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteRequest(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var request = await _requestRepository.Get(id);

                if (request == null)
                {
                    baseResponse.Description = "Request not found";
                    baseResponse.StatusCode = StatusCode.RequestNotFound;
                    return baseResponse;
                }

                await _requestRepository.Delete(request);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteRequest] : {e.Message}",
                    StatusCode = StatusCode.RequestNotFound
                };
            }
        }

        public async Task<IBaseResponse<RequestCreateViewModel>> CreateRequest(RequestCreateViewModel model)
        {
            var baseResponse = new BaseResponse<RequestCreateViewModel>();

            try
            {
                var request = new Request()
                {
                    Date = model.Date,
                    Number = model.Number,
                    CompanyId = model.CompanyId,
                    StoreId = model.StoreId,
                    ClientId = model.ClientId,
                    FiscalId = model.FiscalId,
                    NumberPos = model.NumberPos,
                    ReasonPetition = model.ReasonPetition,
                    EngineerId = model.EngineerId,
                    StatusRequestId = model.StatusRequestId,
                    Works = model.Works,
                    Note = model.Note,
                    TypeRequestId = model.TypeRequestId,
                    StatusBlankId = model.StatusBlankId,
                    AktNumber = model.AktNumber,
                };

                await _requestRepository.Create(request);
            }
            catch (Exception e)
            {
                return new BaseResponse<RequestCreateViewModel>()
                {
                    Description = $"[CreateRequest] : {e.Message}",
                    StatusCode = StatusCode.RequestNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Request>> Edit(int id, RequestEditViewModel model)
        {
            var baseResponse = new BaseResponse<Request>();
            var request = await _requestRepository.Get(id);

            try
            {
                if (request == null)
                {
                    baseResponse.Description = "Request not found";
                    baseResponse.StatusCode = StatusCode.RequestNotFound;
                    return baseResponse;
                }

                request.Date = model.Date;
                request.Number = model.Number;
                request.CompanyId = model.CompanyId;
                request.StoreId = model.StoreId;
                request.ClientId = model.ClientId;
                request.FiscalId = model.FiscalId;
                request.NumberPos = model.NumberPos;
                request.ReasonPetition = model.ReasonPetition;
                request.EngineerId = model.EngineerId;
                request.StatusRequestId = model.StatusRequestId;
                request.Works = model.Works;
                request.Note = model.Note;
                request.TypeRequestId = model.TypeRequestId;
                request.StatusBlankId = model.StatusBlankId;
                request.AktNumber = model.AktNumber;

                await _requestRepository.Update(request);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Request>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.RequestNotFound
                };
            }
        }
    }
}
