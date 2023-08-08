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
    public class FiscalRepository : IFiscalRepository
    {
        private readonly ApplicationDbContext _db;

        public FiscalRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Fiscal entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Fiscal> Get(int id)
        {
            return await _db.Fiscal.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Fiscal>> Select()
        {
            return _db.Fiscal.ToListAsync();
        }

        public async Task<bool> Delete(Fiscal entity)
        {
            _db.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Fiscal> Update(Fiscal entity)
        {
            _db.Fiscal.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<Fiscal> GetByserial(string serial)
        {
            return await _db.Fiscal.FirstOrDefaultAsync(x => x.SerialNumber == serial);
        }

        public Task<List<Fiscal>> GetBySearch(string search)
        {
            var client = from c in _db.Fiscal select c;


            return _db.Fiscal.Where(c => c.SerialNumber.Contains(search)).ToListAsync();
        }
    }
}
