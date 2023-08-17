using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.StatusFiscal
{
    public class StatusFiscalIndexViewModel
    {
        public IEnumerable<Entity.StatusFiscal> StatusFiscals { get; }
        public PageViewModel PageViewModel { get; }

        public StatusFiscalIndexViewModel(List<Entity.StatusFiscal> statusFiscals, PageViewModel viewModel)
        {
            StatusFiscals = statusFiscals;
            PageViewModel = viewModel;

        }
    }
}
