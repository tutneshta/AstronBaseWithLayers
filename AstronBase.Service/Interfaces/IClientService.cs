using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;

namespace AstronBase.Service.Interfaces
{
    public interface IClientService
    {
        Task<IBaseResponse<IEnumerable<Client>>> GetClients();

        Task<IBaseResponse<Client>> GetClient(int id);

        Task<IBaseResponse<Client>> GetClientByName(string name);

        Task<IBaseResponse<bool>> DeleteClient(int id);

        Task<IBaseResponse<ClientViewModel>> CreateClient(ClientViewModel model);

        Task<IBaseResponse<Client>> Edit(int id, ClientViewModel model);

        Task<IBaseResponse<IEnumerable<Client>>> GetClientBySearch(string search);
    }
}