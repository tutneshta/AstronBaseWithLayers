﻿using AstronBase.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.DAL.Interfaces
{
    public interface IEngineerRepository : IBaseRepository<Engineer>
    {
        Task<Engineer> GetByName(string name);

        Task<List<Engineer>> GetBySearch(string search);
    }
}
