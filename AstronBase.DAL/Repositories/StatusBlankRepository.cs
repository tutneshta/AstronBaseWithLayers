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
    public class StatusBlankRepository : IStatusBlankRepository
    {
        private readonly ApplicationDbContext _db;

        public StatusBlankRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(StatusBlank entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<StatusBlank> Get(int id)
        {
            return _db.StatusBlank.FirstOrDefault(m => m.Id == id);
        }

        public async Task<List<StatusBlank>> Select()
        {
            return await _db.StatusBlank.ToListAsync();
        }

        public  async Task<bool> Delete(StatusBlank entity)
        {
             _db.Remove(entity);
             await _db.SaveChangesAsync();
             return true;
        }

        public async Task<StatusBlank> Update(StatusBlank entity)
        {
            _db.StatusBlank.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<StatusBlank> GetByName(string name)
        {
            return await _db.StatusBlank.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<List<StatusBlank>> GetBySearch(string search)
        {
            return await _db.StatusBlank.Where(m => m.Name.Contains(search)).ToListAsync();
        }
    }
}
