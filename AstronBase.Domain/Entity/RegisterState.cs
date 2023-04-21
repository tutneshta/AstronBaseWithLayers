namespace AstronBase.Domain.Entity
{
    public class RegisterState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fiscal> Fiscals { get; set; } = new();
    }
}