using AstronBase.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.DAL.Interfaces
{
    public interface IRequestRepository : IBaseRepository<Request>
    {
        Task<List<Request>> GetBySearch(string search);
    }
}
