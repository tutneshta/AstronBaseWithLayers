using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Store;

namespace AstronBase.Service.Interfaces
{
    public interface IStoreService
    {
        Task<IBaseResponse<IEnumerable<Store>>> GetStores();

        Task<IBaseResponse<Store>> GetStore(int id);

        Task<IBaseResponse<Store>> GetStoreByName(string name);

        Task<IBaseResponse<bool>> DeleteStore(int id);

        Task<IBaseResponse<StoreViewModel>> CreateStore(StoreViewModel model);

        Task<IBaseResponse<Store>> Edit(int id, StoreViewModel model);
    }
}