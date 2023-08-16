using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstronBase.Domain.Entity;

namespace AstronBase.DAL.Interfaces
{
    public interface IStatusBlankRepository : IBaseRepository<StatusBlank>
    {
        Task<StatusBlank> GetByName(string name);

        Task<List<StatusBlank>> GetBySearch(string search);
    }
}
