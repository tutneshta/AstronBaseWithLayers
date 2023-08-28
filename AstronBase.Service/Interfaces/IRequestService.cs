using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Service.Interfaces
{
    public interface IRequestService
    {
        Task<IBaseResponse<IEnumerable<Request>>> GetRequests();

        Task<IBaseResponse<Request>> GetRequest(int id);

        Task<IBaseResponse<IEnumerable<Request>>> GetRequestBySearch(string search);

        Task<IBaseResponse<bool>> DeleteRequest(int id);

        Task<IBaseResponse<RequestCreateViewModel>> CreateRequest(RequestCreateViewModel model);

        Task<IBaseResponse<Request>> Edit(int id, RequestEditViewModel model);

        //Task<IBaseResponse<IEnumerable<Request>>> GetClientBySearch(string search);
    }
}
