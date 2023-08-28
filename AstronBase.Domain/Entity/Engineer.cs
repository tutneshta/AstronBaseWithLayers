using System.ComponentModel;

namespace AstronBase.Domain.Entity
{
    public class Engineer
    {
        public int Id { get; set; }
        [DisplayName ("Имя")]
        public string FirstName { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }

        public List<Fiscal> Fiscals { get; set; } = new();
        public List<Request> Requests { get; set; } = new();
    }
}