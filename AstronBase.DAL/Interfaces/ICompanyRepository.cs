using AstronBase.DAL.Repositories;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AstronBase.DAL.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<Company> GetByName(string name);

        Task<List<Company>> GetBySearch(string search);

    }
}