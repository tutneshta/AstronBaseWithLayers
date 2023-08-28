using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using AstronBase.Domain.Enum;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Store;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Store>>> GetStores()
        {
            var baseResponse = new BaseResponse<IEnumerable<Store>>();

            try
            {
                var store = await _storeRepository.Select();

                if (store.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = store;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Store>>()
                {
                    Description = $"[GetStores] : {e.Message}",
                };
            }
        }

        public async Task<IBaseResponse<Store>> GetStore(int id)
        {
            var baseResponse = new BaseResponse<Store>();

            try
            {
                var store = await _storeRepository.Get(id);

                if (store == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = store;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Store>()
                {
                    Description = $"[GetStore] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Store>> GetStoreByName(string name)
        {
            var baseResponse = new BaseResponse<Store>();

            try
            {
                var store = await _storeRepository.GetByName(name);

                if (store == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = store;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Store>()
                {
                    Description = $"[GetStoreByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteStore(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var store = await _storeRepository.Get(id);

                if (store == null)
                {
                    baseResponse.Description = "Store not found";
                    baseResponse.StatusCode = StatusCode.StoreNotFound;
                    return baseResponse;
                }

                await _storeRepository.Delete(store);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteStore] : {e.Message}",
                    StatusCode = StatusCode.StoreNotFound
                };
            }
        }

        public async Task<IBaseResponse<StoreCreateViewModel>> CreateStore(StoreCreateViewModel model)
        {
            var baseResponse = new BaseResponse<StoreCreateViewModel>();

            try
            {
                var store = new Store()
                {
                    Name = model.Name,

                    //Company = model.Company,
                    CompanyId = model.CompanyId,
                    Id = model.Id
                };

                await _storeRepository.Create(store);
            }
            catch (Exception e)
            {
                return new BaseResponse<StoreCreateViewModel>()
                {
                    Description = $"[CreateStore] : {e.Message}",
                    StatusCode = StatusCode.StoreNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Store>> Edit(int id, StoreEditViewModel model)
        {
            var baseResponse = new BaseResponse<Store>();

            try
            {
                var store = await _storeRepository.Get(id);

                if (store == null)
                {
                    baseResponse.Description = "Store not found";
                    baseResponse.StatusCode = StatusCode.StoreNotFound;
                    return baseResponse;
                }

                store.Name = model.Name;
                store.CompanyId = model.CompanyId;
                store.Id = model.Id;

                await _storeRepository.Update(store);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Store>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.ClientNotFound
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Store>>> GetStoreBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<Store>>();
            try
            {
                var store = await _storeRepository.GetBySearch(search);
                if (store == null)
                {
                    baseResponse.Description = "Store not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = store;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Store>>()
                {
                    Description = $"[GetStoreBySearch] : {e.Message}",

                };
            }
        }
    }
}