using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AstronBase.DAL.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public RequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Request entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Request> Get(int id)
        {
            return await _db.Request
                .Include(c => c.Company)
                .Include(s => s.Store)
                .Include(cl => cl.Client)
                .Include(f => f.Fiscal)
                .Include(e => e.Engineer)
                .Include(s => s.StatusBlank)
                .Include(st => st.StatusRequest)
                .Include(t => t.TypeRequest)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Request>> Select()
        {
            return await _db.Request
                .Include(c => c.Company)
                .Include(s => s.Store)
                .Include(cl => cl.Client)
                .Include(f => f.Fiscal)
                .Include(e => e.Engineer)
                .Include(s => s.StatusBlank)
                .Include(st => st.StatusRequest)
                .Include(t => t.TypeRequest)
                .ToListAsync();
        }

        public async Task<bool> Delete(Request entity)
        {
            _db.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Request> Update(Request entity)
        {
            _db.Request.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }

        //public async Task<Request> GetByNumber(int number)
        //{
        //    return await _db.Request
        //        .Include(c => c.Company)
        //        .Include(s => s.Store)
        //        .Include(cl => cl.Client)
        //        .Include(f => f.Fiscal)
        //        .Include(e => e.Engineer)
        //        .Include(s => s.StatusBlank)
        //        .Include(st => st.StatusRequest)
        //        .Include(t => t.TypeRequest)
        //        .FirstOrDefaultAsync(x => x.Number == number);
        //}

        //public async Task<List<Request>> GetBySearch(string search)
        //{

        //    return await _db.Request
        //        .Include(c => c.Company)
        //        .Include(s => s.Store)
        //        .Include(cl => cl.Client)
        //        .Include(f => f.Fiscal)
        //        .Include(e => e.Engineer)
        //        .Include(s => s.StatusBlank)
        //        .Include(st => st.StatusRequest)
        //        .Include(t => t.TypeRequest)
        //        .FirstOrDefaultAsync(x => x.Number.Equals(search));
        //}

        public  Task<List<Request>> GetBySearch(string search)
        {

            var request = from c in _db.Request select c;


            //return _db.Request.Where(c => c.Number.Equals(search)).ToListAsync();

            return _db.Request
                    .Include(c => c.Company)
                    .Include(s => s.Store)
                    .Include(cl => cl.Client)
                    .Include(f => f.Fiscal)
                    .Include(e => e.Engineer)
                    .Include(s => s.StatusBlank)
                    .Include(st => st.StatusRequest)
                    .Include(t => t.TypeRequest)
                    .Where(x => x.Number.Equals(search)).ToListAsync();
        }
    }
}
