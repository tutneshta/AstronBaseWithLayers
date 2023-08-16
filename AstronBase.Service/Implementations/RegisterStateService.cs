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
using AstronBase.Domain.ViewModels.Engineer;
using AstronBase.Domain.ViewModels.Model;
using AstronBase.Domain.ViewModels.RegisterState;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class RegisterStateService : IRegisterStateService
    {
        private readonly IRegisterStateRepository _registerStateRepository;

        public RegisterStateService(IRegisterStateRepository registerStateRepository)
        {
            _registerStateRepository = registerStateRepository;
        }

        public async Task<IBaseResponse<IEnumerable<RegisterState>>> GetRegisterStates()
        {
            var baseResponse = new BaseResponse<IEnumerable<RegisterState>>();

            try
            {
                var registerStates = await _registerStateRepository.Select();

                if (registerStates.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = registerStates;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<RegisterState>>()
                {
                    Description = $"[GetRegisterStates] : {e.Message}",
                };
            }
        }

        public async Task<IBaseResponse<RegisterState>> GetRegisterState(int id)
        {
            var baseResponse = new BaseResponse<RegisterState>();

            try
            {
                var moregisterStatedel = await _registerStateRepository.Get(id);

                if (moregisterStatedel == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = moregisterStatedel;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<RegisterState>()
                {
                    Description = $"[GetRegisterState] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<RegisterState>> GetRegisterStateByName(string name)
        {
            var baseResponse = new BaseResponse<RegisterState>();

            try
            {
                var registerState = await _registerStateRepository.GetByName(name);

                if (registerState == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = registerState;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<RegisterState>()
                {
                    Description = $"[GetRegisterStateByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteRegisterState(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var registerState = await _registerStateRepository.Get(id);

                if (registerState == null)
                {
                    baseResponse.Description = "RegisterStateNotFound not found";
                    baseResponse.StatusCode = StatusCode.RegisterStateNotFound;
                    return baseResponse;
                }

                await _registerStateRepository.Delete(registerState);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteRegisterState] : {e.Message}",
                    StatusCode = StatusCode.RegisterStateNotFound
                };
            }
        }

        public async Task<IBaseResponse<RegisterStateCreateViewModel>> CreateRegisterState(RegisterStateCreateViewModel model)
        {
            var baseResponse = new BaseResponse<RegisterStateCreateViewModel>();

            try
            {
                var registerState = new RegisterState()
                {

                    Id = model.Id,
                    Name = model.Name,

                };

                await _registerStateRepository.Create(registerState);
            }
            catch (Exception e)
            {
                return new BaseResponse<RegisterStateCreateViewModel>()
                {
                    Description = $"[CreateRegisterState] : {e.Message}",
                    StatusCode = StatusCode.RegisterStateNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<RegisterState>> Edit(int id, RegisterStateEditViewModel model)
        {
            var baseResponse = new BaseResponse<RegisterState>();

            try
            {
                var registerState = await _registerStateRepository.Get(id);

                if (registerState == null)
                {
                    baseResponse.Description = "RegisterStateNotFound not found";
                    baseResponse.StatusCode = StatusCode.RegisterStateNotFound;
                    return baseResponse;
                }

                registerState.Id = model.Id;
                registerState.Name = model.Name;
             


                await _registerStateRepository.Update(registerState);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<RegisterState>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.RegisterStateNotFound
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<RegisterState>>> GetRegisterStateBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<RegisterState>>();
            try
            {
                var registerState = await _registerStateRepository.GetBySearch(search);
                if (registerState == null)
                {
                    baseResponse.Description = "RegisterStateNotFound not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = registerState;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<RegisterState>>()
                {
                    Description = $"[GetRegisterStateBySearch] : {e.Message}",

                };
            }
        }
    }
}
