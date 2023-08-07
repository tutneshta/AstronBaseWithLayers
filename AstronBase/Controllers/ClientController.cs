using AstronBase.DAL;
using AstronBase.Domain.ViewModels.Client;
using AstronBase.Domain.ViewModels.Pagination;

using AstronBase.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AstronBase.Controllers
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

        private void CompaniesDropDownList(object selectedCompany = null)
        {
            var companyQuery = from d in _db.Company
                orderby d.Name
                select d;

            ViewBag.CompanyId = new SelectList(companyQuery, "Id", "Name", selectedCompany);
        }

        private void StoresDropDownList(object selectedStore = null)
        {
            var storeQuery = from d in _db.Store
                orderby d.Name
                select d;

            ViewBag.StoreId = new SelectList(storeQuery, "Id", "Name", selectedStore);
        }


        /// <summary>
        /// request to withdraw all clients
        /// </summary>
        /// <returns></returns>
        // GET
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
 
            ViewBag.CurrentFilter = searchString;

            var response = await _clientService.GetClients();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _clientService.GetClientBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new ClientIndexViewModel(items, pageViewModel);

            return View(viewModel);
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
            CompaniesDropDownList();
            StoresDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _clientService.CreateClient(model);

                    return RedirectToAction("Index");
                }

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
        public async Task<IActionResult> Edit(int id, ClientEditViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }

            return Redirect("Error");
        }
    }
}