using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstronBase.Domain.Entity;

namespace AstronBase.DAL.Interfaces
{
    public interface IModelRepository : IBaseRepository<Model>
    {
        Task<Model> GetByName(string name);

        Task<List<Model>> GetBySearch(string search);
    }
}
