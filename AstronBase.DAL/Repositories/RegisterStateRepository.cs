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
    public class RegisterStateRepository : IRegisterStateRepository
    {
        private readonly ApplicationDbContext _db;

        public RegisterStateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(RegisterState entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<RegisterState> Get(int id)
        {
            return  _db.RegisterState.FirstOrDefault(m => m.Id == id);
        }

        public Task<List<RegisterState>> Select()
        {
            return _db.RegisterState.ToListAsync();
        }

        public async Task<bool> Delete(RegisterState entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<RegisterState> Update(RegisterState entity)
        {
            _db.RegisterState.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ClearFk(RegisterState entity)
        {
            var fiscals = _db.Fiscal;
            foreach (var fiscal in fiscals)
            {
                if (fiscal.ModelId == entity.Id)
                {
                    fiscal.ModelId = null;
                }

            }

            return true;
        }


        public async Task<RegisterState> GetByName(string name)
        {
            return await _db.RegisterState.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<List<RegisterState>> GetBySearch(string search)
        {
            return await _db.RegisterState.Where(m => m.Name.Contains(search)).ToListAsync();
        }
    }
}
