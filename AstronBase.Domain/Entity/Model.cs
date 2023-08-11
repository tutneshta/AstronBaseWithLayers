using System.ComponentModel;

namespace AstronBase.Domain.Entity
{
    public class Model
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
        public List<Fiscal> Fiscals { get; set; } = new();
    }
}