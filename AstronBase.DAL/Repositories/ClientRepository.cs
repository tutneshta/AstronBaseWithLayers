﻿using AstronBase.DAL.Interfaces;
using AstronBase.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AstronBase.DAL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _db;

        public ClientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Client entity)
        {
            await _db.AddAsync(entity);

            await _db.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// get Client in id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Client> Get(int id)
        {
            return await _db.Client.Include(r => r.Company)
                .Include(s => s.Store)
                . FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// select all client
        /// </summary>
        /// <returns></returns>
        public Task<List<Client>> Select()
        {
            return _db.Client
                .Include(c => c.Company)
                .Include(s => s.Store)
                .ToListAsync();
        }

        /// <summary>
        /// delete client
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Client entity)
        {
            

            _db.Remove(entity);

            await _db.SaveChangesAsync();

            return true;
        }



        /// <summary>
        /// update Client
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<Client> Update(Client entity)
        {
            _db.Client.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }

        public Task<bool> ClearFk(Client entity)
        {


            return Task.FromResult(true);
        }


        /// <summary>
        /// get Client in name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Client> GetByName(string name)
        {
            return await _db.Client.FirstOrDefaultAsync(x => x.Name == name);
        }

        public Task<List<Client>> GetBySearch(string search)
        {

            var client = from c in _db.Client select c;


            return _db.Client.Where(c => c.Name.Contains(search)).ToListAsync();
        }
    }
}