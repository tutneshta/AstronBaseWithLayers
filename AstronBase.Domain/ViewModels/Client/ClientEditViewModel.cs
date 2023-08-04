using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Client
{
    public class ClientEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CompanyId { get; set; }

        public int StoreId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
