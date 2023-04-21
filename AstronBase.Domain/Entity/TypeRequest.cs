namespace AstronBase.Domain.Entity
{
    public class TypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Request> Requests { get; set; } = new();
    }
}