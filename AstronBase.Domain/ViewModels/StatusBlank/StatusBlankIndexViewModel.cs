using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.StatusBlank
{
    public class StatusBlankIndexViewModel
    {
        public IEnumerable<Entity.StatusBlank> StatusBlanks { get; }
        public PageViewModel PageViewModel { get; }

        public StatusBlankIndexViewModel(List<Entity.StatusBlank> statusBlanks, PageViewModel viewModel)
        {
            StatusBlanks = statusBlanks;
            PageViewModel = viewModel;

        }
    }
}
