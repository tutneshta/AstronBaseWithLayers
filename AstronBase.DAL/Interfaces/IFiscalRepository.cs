using AstronBase.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.DAL.Interfaces
{
    public interface IFiscalRepository : IBaseRepository<Fiscal>
    {
        Task<Fiscal> GetByserial(string serial);

        Task<List<Fiscal>> GetBySearch(string search);
    }
}
