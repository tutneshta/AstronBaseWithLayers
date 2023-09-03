using System.Collections;

namespace AstronBase.Domain.Entity
{
    public class Client 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? StoreId { get; set; }
        public Store Store { get; set; }



    }
}