using System.Collections;

namespace AstronBase.Domain.Entity
{
    public class Client 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Company> Company { get; set; } = new();


    }
}