using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AstronBase.DAL.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Store entity)
        {
            await _db.Store.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Store> Get(int id)
        {
            return await _db.Store.Include(c => c.Company).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Store>> Select()
        {
            return _db.Store.Include(c => c.Company).ToListAsync();
        }

        public async Task<bool> Delete(Store entity)
        {
            _db.Store.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Store> Update(Store entity)
        {
            _db.Store.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<Store> GetByName(string name)
        {
            return await _db.Store.Include(c => c.Company).FirstOrDefaultAsync(x => x.Name == name);
        }

        public Task<List<Store>> GetBySearch(string search)
        {
            var client = from c in _db.Store select c;

            return _db.Store.Where(c => c.Name.Contains(search))
                .Include(c => c.Company)
                .ToListAsync();
        }

        public  Task<List<Company>> CompaniesDropDownList(string search)
        {

            IOrderedQueryable<Company> companyQuery = from d in _db.Company
                orderby d.Name
                select d;

            return companyQuery.ToListAsync();
        }


    }
}