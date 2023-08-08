using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronBase.Domain.ViewModels.Fiscal
{
    public class FiscalEditViewModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int RegisterStateId { get; set; }

        public int ModelId { get; set; }

        public int? StatusFiscalId { get; set; }

        public int? CompanyId { get; set; }

        public int? StoreId { get; set; }

        public int? EngineerId { get; set; }

        public string Note { get; set; }
    }
}
