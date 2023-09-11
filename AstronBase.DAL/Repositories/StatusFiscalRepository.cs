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
    public class StatusFiscalRepository : IStatusFiscalRepository
    {
        private readonly ApplicationDbContext _db;

        public StatusFiscalRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<bool> Create(StatusFiscal entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<StatusFiscal> Get(int id)
        {
            return _db.StatusFiscal.FirstOrDefault(m => m.Id == id);
        }

        public async Task<List<StatusFiscal>> Select()
        {
            return await _db.StatusFiscal.ToListAsync();
        }

        public async Task<bool> Delete(StatusFiscal entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<StatusFiscal> Update(StatusFiscal entity)
        {
            _db.StatusFiscal.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ClearFk(StatusFiscal entity)
        {
            var fiscals = _db.Fiscal;
            foreach (var fiscal in fiscals)
            {
                if (fiscal.StatusFiscalId == entity.Id)
                {
                    fiscal.StatusFiscalId = null;
                }

            }

            return true;
        }


        public async Task<StatusFiscal> GetByName(string name)
        {
            return await _db.StatusFiscal.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<List<StatusFiscal>> GetBySearch(string search)
        {
            return await _db.StatusFiscal.Where(m => m.Name.Contains(search)).ToListAsync();
        }
    }
}
