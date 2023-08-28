using System.ComponentModel;

namespace AstronBase.Domain.Entity
{
    public class StatusBlank
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }
        public List<Request> Requests { get; set; } = new();
    }
}