using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Model
{
    public class ModelIndexViewModel
    {
        public IEnumerable<Entity.Model> Models { get; }
        public PageViewModel PageViewModel { get; }

        public ModelIndexViewModel(List<Entity.Model> models, PageViewModel viewModel)
        {
            Models = models;
            PageViewModel = viewModel;

        }
    }
}
