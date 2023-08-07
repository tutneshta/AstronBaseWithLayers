using AstronBase.Domain.Entity;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Company;

namespace AstronBase.Service.Interfaces
{
    public interface ICompanyService
    {
        Task<IBaseResponse<IEnumerable<Company>>> GetCompanies();

        Task<IBaseResponse<Company>> GetCommpany(int id);

        Task<IBaseResponse<Company>> GetCompanyByName(string name);

        Task<IBaseResponse<bool>> DeleteCompany(int id);

        Task<IBaseResponse<CompanyCreateViewModel>> CreateCompany(CompanyCreateViewModel model);

        Task<IBaseResponse<Company>> Edit(int id, CompanyEditViewModel model);

        Task<IBaseResponse<IEnumerable<Company>>> GetCompanyBySearch(string search);
    }
}