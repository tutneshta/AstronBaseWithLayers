using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AstronBase.DAL.Repositories
{
    public class EngineerRepository : IEngineerRepository
    {
        private readonly ApplicationDbContext _db;

        public EngineerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Engineer entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Engineer> Get(int id)
        {
            return await _db.Engineer.Include(c => c.Requests)
                .Include(s => s.Fiscals)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Engineer>> Select()
        {
            return _db.Engineer
                .Include(c => c.Requests)
                .Include(s => s.Fiscals)
                .ToListAsync();
        }

        public async Task<bool> Delete(Engineer entity)
        {
            _db.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Engineer> Update(Engineer entity)
        {
            _db.Engineer.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<Engineer> GetByName(string name)
        {
            return await _db.Engineer
                .Include(f => f.Fiscals)
                .Include(r => r.Requests)
                .FirstOrDefaultAsync(x => x.FirstName == name);
        }

        public Task<List<Engineer>> GetBySearch(string search)
        {

            var client = from c in _db.Client select c;


            return _db.Engineer.Where(c => c.FirstName.Contains(search))
                .Include(r => r.Requests)
                .Include(f => f.Fiscals)
                .ToListAsync();
        }
    }
}
