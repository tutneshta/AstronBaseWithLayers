using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Store
{
    public class StoreIndexViewModel
    {
        public IEnumerable<Entity.Store> Stores { get; }
        public PageViewModel PageViewModel { get; }

        public StoreIndexViewModel(List<Entity.Store> stores, PageViewModel viewModel)
        {
            Stores = stores;
            PageViewModel = viewModel;
        }
    }
}
