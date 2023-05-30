using AstronBase.Domain.ViewModels.Pagination;

namespace AstronBase.Domain.ViewModels.Company
{
    public class CompanyIndexViewModel
    {
        public IEnumerable<Entity.Company> Companies { get; }
        public PageViewModel PageViewModel { get; }

        public CompanyIndexViewModel(IEnumerable<Entity.Company> companies, PageViewModel viewModel)
        {
            Companies = companies;
            PageViewModel = viewModel;
        }
    }
}