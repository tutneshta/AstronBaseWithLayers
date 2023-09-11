using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace AstronBase.DAL.Repositories
{
    public class InClassName
    {
        public InClassName(Company entity)
        {
            Entity = entity;
        }

        public Company Entity { get; private set; }
        public DbSet<Store> Stores { get; set; }
    }

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

            ClearFk(entity);

            _db.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public Task<bool> ClearFk(Company entity)
        {
            var stores = _db.Store;
            foreach (var store in stores)
            {
                if (store.CompanyId == entity.Id)
                {
                    store.CompanyId = null;
                }

            }

            var clients = _db.Client;
            foreach (var client in clients)
            {
                if (client.CompanyId == entity.Id)
                {
                    client.CompanyId = null;
                }

            }

            var fiscals  = _db.Fiscal;
            foreach (var fiscal in fiscals)
            {
                if (fiscal.CompanyId == entity.Id)
                {
                    fiscal.CompanyId = null;
                }

            }

            var requests = _db.Request;
            foreach (var request in requests)
            {
                if (request.CompanyId == entity.Id)
                {
                    request.CompanyId = null;
                }

            }

            return Task.FromResult(true);
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