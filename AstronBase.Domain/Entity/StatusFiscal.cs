namespace AstronBase.Domain.Entity
{
    public class StatusFiscal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fiscal> Fiscals { get; set; } = new();
    }
}