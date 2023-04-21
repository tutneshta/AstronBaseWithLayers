namespace AstronBase.Domain.Entity
{
    public class Engineer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Fiscal> Fiscals { get; set; } = new();
        public List<Request> Requests { get; set; } = new();
    }
}