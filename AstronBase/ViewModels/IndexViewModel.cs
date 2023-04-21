using AstronBase.Domain.Entity;
using AstronBase.Models;

namespace AstronBase.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Company> Companies { get; }
        public PageViewModel PageViewModel { get; }

        public IndexViewModel(IEnumerable<Company> companies, PageViewModel viewModel)
        {
            Companies = companies;
            PageViewModel = viewModel;
        }
    }
}