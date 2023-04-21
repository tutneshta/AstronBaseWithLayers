using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using AstronBase.Domain.Enum;
using AstronBase.Domain.Response;
using AstronBase.Domain.ViewModels.Company;
using AstronBase.Service.Interfaces;

namespace AstronBase.Service.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository clientRepository)
        {
            _companyRepository = clientRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Company>>> GetCompanies()
        {
            var baseResponse = new BaseResponse<IEnumerable<Company>>();

            try
            {
                var companies = await _companyRepository.Select();

                if (companies.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                }

                baseResponse.Data = companies;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Company>>()
                {
                    Description = $"[GetCompanies] : {e.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Company>> GetCommpany(int id)
        {
            var baseResponse = new BaseResponse<Company>();

            try
            {
                var company = await _companyRepository.Get(id);

                if (company == null)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = company;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Company>()
                {
                    Description = $"[GetCommpany] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Company>> GetCompanyByName(string name)
        {
            var baseResponse = new BaseResponse<Company>();

            try
            {
                var company = await _companyRepository.GetByName(name);

                if (company == null)
                {
                    baseResponse.Description = "Company not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = company;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Company>()
                {
                    Description = $"[GetCompanyByName] : {e.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Company>>> GetCompanyBySearch(string search)
        {
            var baseResponse = new BaseResponse<IEnumerable<Company>>();
            try
            {
                var company = await _companyRepository.GetBySearch(search);
                if (company == null)
                {
                    baseResponse.Description = "Company not found";
                    baseResponse.StatusCode = StatusCode.InternalServerError;
                    return baseResponse;
                }

                baseResponse.Data = company;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<IEnumerable<Company>>()
                {
                    Description = $"[GetCompanyBySearch] : {e.Message}",
                    
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteCompany(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var company = await _companyRepository.Get(id);

                if (company == null)
                {
                    baseResponse.Description = "Company not found";
                    baseResponse.StatusCode = StatusCode.CompanyNotFound;
                    return baseResponse;
                }

                await _companyRepository.Delete(company);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCompany] : {e.Message}",
                    StatusCode = StatusCode.CompanyNotFound
                };
            }
        }

        public async Task<IBaseResponse<CompanyViewModel>> CreateCompany(CompanyViewModel model)
        {
            var baseResponse = new BaseResponse<CompanyViewModel>();

            try
            {
                var company = new Company()
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Bank = model.Bank,
                    DirectorName = model.DirectorName,
                    PositionName = model.PositionName,
                    StatuteName = model.StatuteName,
                    Unn = model.Unn,
                    Note = model.Note,
                    Email = model.Email,
                    CheckingAccount = model.CheckingAccount
                };

                await _companyRepository.Create(company);
            }
            catch (Exception e)
            {
                return new BaseResponse<CompanyViewModel>()
                {
                    Description = $"[CreateCompany] : {e.Message}",
                    StatusCode = StatusCode.CompanyNotFound
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<Company>> Edit(int id, CompanyViewModel model)
        {
            var baseResponse = new BaseResponse<Company>();
            var company = await _companyRepository.Get(id);

            try
            {
                if (company == null)
                {
                    baseResponse.Description = "Company not found";
                    baseResponse.StatusCode = StatusCode.CompanyNotFound;
                    return baseResponse;
                }

                company.Name = model.Name;
                company.PhoneNumber = model.PhoneNumber;
                company.Address = model.Address;
                company.Bank = model.Bank;
                company.DirectorName = model.DirectorName;
                company.PositionName = model.PositionName;
                company.StatuteName = model.StatuteName;
                company.Unn = model.Unn;
                company.Note = model.Note;
                company.Email = model.Email;
                company.CheckingAccount = model.CheckingAccount;

                await _companyRepository.Update(company);

                return baseResponse;
            }
            catch (Exception e)
            {
                return new BaseResponse<Company>()
                {
                    Description = $"[Edit] : {e.Message}",
                    StatusCode = StatusCode.CompanyNotFound
                };
            }
        }
    }
}