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
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext _db;

        public ModelRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Model entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Model> Get(int id)
        {
            return _db.Model.FirstOrDefault(m => m.Id == id);
        }

        public Task<List<Model>> Select()
        {
            return _db.Model.ToListAsync();
        }

        public async Task<bool> Delete(Model entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Model> Update(Model entity)
        {
            _db.Model.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ClearFk(Model entity)
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


        public async Task<Model> GetByName(string name)
        {
            return await _db.Model.FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<List<Model>> GetBySearch(string search)
        {
            return await _db.Model.Where(m => m.Name.Contains(search)).ToListAsync();
        }
    }
}
