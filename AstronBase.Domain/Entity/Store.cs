namespace AstronBase.Domain.Entity
{
    public class Store 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        //public string Address { get; set; }

        public List<Request> Requests { get; set; } = new();
        public List<Client> Clients { get; set; } = new();


    }
}