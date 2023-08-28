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
using AstronBase.Domain.ViewModels.Store;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class EngineerService : IEngineerService
    {
        private readonly IEngineerRepository _engineerRepository;

        public EngineerService(IEngineerRepository engineerRepository)
        {
            _engineerRepository = engineerRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Engineer>>> GetEngineers()
        {
            var baseResponse = new BaseResponse<IEnumerable<Engineer>>();

            try
            {
                var engineers = await _engineerRepository.Select();

                if (engineers.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = engineers;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Engineer>>()
                {
                    Description = $"[GetEngineers] : {e.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Engineer>> GetEngineer(int id)
        {
            var baseResponse = new BaseResponse<Engineer>();

            try
            {
                var engineer = await _engineerRepository.Get(id);

                if (engineer == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = engineer;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Engineer>()
                {
                    Description = $"[GetEngineer] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Engineer>> GetEngineerByName(string name)
        {
            var baseResponse = new BaseResponse<Engineer>();

            try
            {
                var engineer = await _engineerRepository.GetByName(name);

                if (engineer == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = engineer;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Engineer>()
                {
                    Description = $"[GetEngineerByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteEngineer(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var engineer = await _engineerRepository.Get(id);

                if (engineer == null)
                {
                    baseResponse.Description = "Engineer not found";
                    baseResponse.StatusCode = StatusCode.EngineerNotFound;
                    return baseResponse;
                }

                await _engineerRepository.Delete(engineer);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteEngineer] : {e.Message}",
                    StatusCode = StatusCode.StoreNotFound
                };
            }
        }

        public async Task<IBaseResponse<EngineerCreateViewModel>> CreateEngineer(EngineerCreateViewModel model)
        {
            var baseResponse = new BaseResponse<EngineerCreateViewModel>();

            try
            {
                var engineer = new Engineer()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    
                };

                await _engineerRepository.Create(engineer);
            }
            catch (Exception e)
            {
                return new BaseResponse<EngineerCreateViewModel>()
                {
                    Description = $"[CreateEngineer] : {e.Message}",
                    StatusCode = StatusCode.EngineerNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Engineer>> Edit(int id, EngineerEditViewModel model)
        {
            var baseResponse = new BaseResponse<Engineer>();

            try
            {
                var engineer = await _engineerRepository.Get(id);

                if (engineer == null)
                {
                    baseResponse.Description = "Engineer not found";
                    baseResponse.StatusCode = StatusCode.EngineerNotFound;
                    return baseResponse;
                }

                engineer.Id = model.Id;
                engineer.FirstName = model.FirstName;
                engineer.LastName = model.LastName;
                engineer.Email = model.Email;
                engineer.PhoneNumber = model.PhoneNumber;


                await _engineerRepository.Update(engineer);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Engineer>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.EngineerNotFound
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Engineer>>> GetEngineerBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<Engineer>>();
            try
            {
                var engineer = await _engineerRepository.GetBySearch(search);
                if (engineer == null)
                {
                    baseResponse.Description = "Engineer not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = engineer;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Engineer>>()
                {
                    Description = $"[GetEngineerBySearch] : {e.Message}",

                };
            }
        }
    }
}
