using AstronBase.Domain.ViewModels.Pagination;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Client
{
    public class ClientCreateViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CompanyId { get; set; }
        public string PhoneNumber { get; set; }

        //public IEnumerable Clients { get; }
        //public PageViewModel PageViewModel { get; }
    }
}
