using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Request
{
    public class RequestIndexViewModel
    {
        public IEnumerable<Entity.Request> Requests { get; }
        public PageViewModel PageViewModel { get; }

        public RequestIndexViewModel(List<Entity.Request> requests, PageViewModel viewModel)
        {
            Requests = requests;
            PageViewModel = viewModel;
        }
    }
}
