using AstronBase.Domain.Entity;

namespace AstronBase.DAL.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task<Client> GetByName(string name);

        Task<List<Client>> GetBySearch(string search);
    }
}