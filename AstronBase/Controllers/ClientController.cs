using AstronBase.Domain.ViewModels.Client;
using AstronBase.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AstronBase.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;


        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// request to withdraw all clients
        /// </summary>
        /// <returns></returns>
        // GET
        public async Task<IActionResult> Index()
        {
            var response = await _clientService.GetClients();

            //if (response.StatusCode == Domain.Enum.StatusCode.OK)
            //{
                return View(response.Data);
            //}

           // return Redirect("Error");
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _clientService.GetClient(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _clientService.CreateClient(model);
                    return RedirectToAction("Index");
                }

                await _clientService.Edit(model.Id, model);
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (_clientService.GetClient(id) == null)
            {
                return NotFound();
            }

            var response = await _clientService.GetClient(id);

            if (response == null)
            {
                return NotFound();
            }

            return View(response.Data);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _clientService.GetClient(id);

            if (response == null)
            {
                return Problem("Entity set 'AstronBaseContext.Client'  is null.");
            }

            if (response.Data != null)
            {
                await _clientService.DeleteClient(id);
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _clientService.GetClient(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _clientService.Edit(model.Id, model);
                }
                else
                {
                    await _clientService.Edit(model.Id, model);
                }

                return RedirectToAction("Index");
            }

            return Redirect("Error");
        }
    }
}