using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Fiscal
{
    public class FiscalIndexViewModel
    {
        public IEnumerable<Entity.Fiscal> Fiscals { get; }
        public PageViewModel PageViewModel { get; }

        public FiscalIndexViewModel(List<Entity.Fiscal> fiscals, PageViewModel viewModel)
        {
            Fiscals = fiscals;
            PageViewModel = viewModel;
        }
    }
}
