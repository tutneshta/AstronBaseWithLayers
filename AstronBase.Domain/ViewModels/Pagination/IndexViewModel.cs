namespace AstronBase.Domain.ViewModels.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<Entity.Company> Companies { get; }
        public PageViewModel PageViewModel { get; }

        public IndexViewModel(IEnumerable<Entity.Company> companies, PageViewModel viewModel)
        {
            Companies = companies;
            PageViewModel = viewModel;
        }
    }
}