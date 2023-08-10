using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Engineer
{
    public class EngineerIndexViewModel
    {
        public IEnumerable<Entity.Engineer> Engineers { get; }
        public PageViewModel PageViewModel { get; }

        public EngineerIndexViewModel(List<Entity.Engineer> engineers, PageViewModel viewModel)
        {
            Engineers = engineers;
            PageViewModel = viewModel;

        }
    }
}
