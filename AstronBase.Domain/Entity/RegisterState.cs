using System.ComponentModel;

namespace AstronBase.Domain.Entity
{
    public class RegisterState
    {
        public int Id { get; set; }

        [DisplayName("Название статуса")]
        public string Name { get; set; }
        public List<Fiscal> Fiscals { get; set; } = new();
    }
}