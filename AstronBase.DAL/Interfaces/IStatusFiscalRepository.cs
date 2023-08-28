using AstronBase.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.DAL.Interfaces
{
    public interface IStatusFiscalRepository : IBaseRepository<StatusFiscal>
    {
        Task<StatusFiscal> GetByName(string name);

        Task<List<StatusFiscal>> GetBySearch(string search);
    }
}
