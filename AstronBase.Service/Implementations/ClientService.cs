using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using AstronBase.Domain.Enum;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Client>>> GetClients()
        {
            var baseResponse = new BaseResponse<IEnumerable<Client>>();

            try
            {
                var clients = await _clientRepository.Select();

                if (clients.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = clients;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Client>>()
                {
                    Description = $"[GetClients] : {e.Message}"
                };
            }
        }

        /// <summary>
        /// получение клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IBaseResponse<Client>> GetClient(int id)
        {
            var baseResponse = new BaseResponse<Client>();

            try
            {
                var client = await _clientRepository.Get(id);

                if (client == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = client;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Client>()
                {
                    Description = $"[GetClient] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// получение клиента по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IBaseResponse<Client>> GetClientByName(string name)
        {
            var baseResponse = new BaseResponse<Client>();

            try
            {
                var client = await _clientRepository.GetByName(name);

                if (client == null)
                {
                    baseResponse.Description = "Client not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = client;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Client>()
                {
                    Description = $"[GetClientByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// удаление клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IBaseResponse<bool>> DeleteClient(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var client = await _clientRepository.Get(id);

                if (client == null)
                {
                    baseResponse.Description = "Client not found";
                    baseResponse.StatusCode = StatusCode.ClientNotFound;
                    return baseResponse;
                }

                await _clientRepository.Delete(client);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteClient] : {e.Message}",
                    StatusCode = StatusCode.ClientNotFound
                };
            }
        }

        public async Task<IBaseResponse<ClientViewModel>> CreateClient(ClientViewModel model)
        {
            var baseResponse = new BaseResponse<ClientViewModel>();

            try
            {
                var client = new Client()
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber
                };

                await _clientRepository.Create(client);
            }
            catch (Exception e)
            {
                return new BaseResponse<ClientViewModel>()
                {
                    Description = $"[CreateClient] : {e.Message}",
                    StatusCode = StatusCode.ClientNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Client>> Edit(int id, ClientViewModel model)
        {
            var baseResponse = new BaseResponse<Client>();
            var client = await _clientRepository.Get(id);

            try
            {
                if (client == null)
                {
                    baseResponse.Description = "Client not found";
                    baseResponse.StatusCode = StatusCode.ClientNotFound;
                    return baseResponse;
                }


                client.Name = model.Name;
                client.PhoneNumber = model.PhoneNumber;
                await _clientRepository.Update(client);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Client>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.ClientNotFound
                };
            }
        }
    }
}