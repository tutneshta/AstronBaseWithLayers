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
            return await _db.Fiscal.Include(c => c.Company)
                .Include(s => s.Store)
                .Include(e => e.Engineer)
                .Include(s => s.StatusFiscal)
                .Include(m => m.Model)
                .Include(r => r.RegisterState)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Fiscal>> Select()
        {
            return await _db.Fiscal.Include(c => c.Company)
                .Include(s => s.Store)
                .Include(e => e.Engineer)
                .Include(s => s.StatusFiscal)
                .Include(m => m.Model)
                .Include(r => r.RegisterState)
                .ToListAsync();
        }

        public async Task<bool> Delete(Fiscal entity)
        {
            await ClearFk(entity);

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

        public async Task<bool> ClearFk(Fiscal entity)
        {
            var requests = _db.Request;
            foreach (var request in requests)
            {
                if (request.FiscalId == entity.Id)
                {
                    request.FiscalId = null;
                }

            }

            return true;
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
