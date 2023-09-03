using AstronBase.DAL;
using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.Pagination;
using AstronBase.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace API.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly ApplicationDbContext _db;
       


        public ClientController(IClientService clientService, ApplicationDbContext db)
        {
            _clientService = clientService;
            _db = db;
        }

        /// <summary>
        /// Получение всех клиентов
        /// </summary>
        /// /// <remarks>
        /// need administrator rights</remarks>
        /// <response code="200">Возвращает список клиентов</response>
        /// <response code="404">Необходимы права администратора</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("GetClients")]
        public async Task<IEnumerable<Client>> GetClients()
        {
            //IBaseResponse<IEnumerable<Client>> clients =  await _clientService.GetClients();

            //return clients.Data.ToList();
            var clients = _db.Client.ToList();
            return clients;
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// /// <remarks>
        /// need administrator rights</remarks>
        /// <response code="200">Возвращает статус ОК</response>
        /// <response code="404">Необходимы права администратора</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("RemoveClient")]
        public async Task<IActionResult> RemoveClient(int id)
        {
            await _clientService.DeleteClient(id);

            return StatusCode(201);
        }


    }
}