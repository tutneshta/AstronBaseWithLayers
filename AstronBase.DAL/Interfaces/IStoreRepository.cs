﻿using AstronBase.Domain.Entity;

namespace AstronBase.DAL.Interfaces
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        Task<Store> GetByName(string name);

        Task<List<Store>> GetBySearch(string search);
    }
}