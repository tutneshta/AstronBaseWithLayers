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
            ClearFk(entity);

            await ClearFk(entity);

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

        public async Task<bool> ClearFk(Store entity)
        {
            var clients = _db.Client;
            foreach (var client in clients)
            {
                if (client.StoreId == entity.Id)
                {
                    client.StoreId = null;
                }

            }

            var requests = _db.Request;
            foreach (var request in requests)
            {
                if (request.StoreId == entity.Id)
                {
                    request.StoreId = null;
                }

            }

            var fiscals = _db.Fiscal;
            foreach (var fiscal in fiscals)
            {
                if (fiscal.StoreId == entity.Id)
                {
                    fiscal.StoreId = null;
                }

            }

            return true;
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