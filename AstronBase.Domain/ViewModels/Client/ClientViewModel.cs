using System.Collections;
using AstronBase.Domain.ViewModels.Pagination;

namespace AstronBase.Domain.ViewModels.Client
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable Clients { get; }
        public PageViewModel PageViewModel { get; }

    }


}