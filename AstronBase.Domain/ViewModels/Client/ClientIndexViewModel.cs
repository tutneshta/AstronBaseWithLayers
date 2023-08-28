using AstronBase.Domain.ViewModels.Pagination;

namespace AstronBase.Domain.ViewModels.Client
{
    public class ClientIndexViewModel
    {
        public IEnumerable<Entity.Client> Clients { get; }
        public PageViewModel PageViewModel { get; }

        public ClientIndexViewModel(List<Entity.Client> clients, PageViewModel viewModel)
        {
            Clients = clients;
            PageViewModel = viewModel;
        }
    }
}