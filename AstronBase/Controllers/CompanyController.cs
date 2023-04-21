using Microsoft.AspNetCore.Mvc;
using AstronBase.Domain.ViewModels.Company;
using AstronBase.Domain.ViewModels.Pagination;
using AstronBase.Service.Interfaces;


namespace AstronBase.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
            ViewBag.CurrentFilter = searchString;

            var response = await _companyService.GetCompanies();

            if (!string.IsNullOrEmpty(searchString))
            {

                response = await _companyService.GetCompanyBySearch(searchString);

            }

            const int pageSize = 5;

            var count = response.Data.Count();

            var items = response.Data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var viewModel = new IndexViewModel(items, pageViewModel);

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {

            var response = await _companyService.GetCommpany(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return Redirect("Error");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyViewModel model)

        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _companyService.CreateCompany(model);

                    return RedirectToAction("Index");
                }

                await _companyService.Edit(model.Id, model);
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
     
            var response = await _companyService.GetCommpany(id);

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyViewModel model)
        {
     
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _companyService.CreateCompany(model);
                }
                else
                {
                    await _companyService.Edit(model.Id, model);
                }

                return RedirectToAction("Index");
            }

            return Redirect("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
  
            if (_companyService.GetCommpany(id) == null)
            {
                return NotFound();
            }

            var response = await _companyService.GetCommpany(id);

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

            var response = await _companyService.GetCommpany(id);

            if (response == null)
            {
                return Problem("Entity set 'AstronBaseContext.Client'  is null.");
            }

            if (response.Data != null)
            {
                await _companyService.DeleteCompany(id);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}