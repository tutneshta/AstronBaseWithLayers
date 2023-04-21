using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AstronBase.DAL.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Company entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Company> Get(int id)
        {
            return await _db.Company.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Company>> Select()
        {
            return _db.Company.ToListAsync();
        }

        public async Task<bool> Delete(Company entity)
        {
            _db.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Company> Update(Company entity)
        {
            _db.Company.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<Company> GetByName(string name)
        {
            return await _db.Company.FirstOrDefaultAsync(x => x.Name == name);
        }

        public    Task<List<Company>> GetBySearch(string search)
        {
            var companies = from c in  _db.Company select c;

           
               // return companies.Where(s =>
                   // s.Name.Contains(search) || s.DirectorName.Contains(search));

                   return  _db.Company.Where(c => c.Name.Contains(search)).ToListAsync();

        }
    }
}