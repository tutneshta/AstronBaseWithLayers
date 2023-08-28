using AstronBase.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.DAL.Interfaces
{
    public interface IRegisterStateRepository : IBaseRepository<RegisterState>
    {
        Task<RegisterState> GetByName(string name);

        Task<List<RegisterState>> GetBySearch(string search);
    }
}
