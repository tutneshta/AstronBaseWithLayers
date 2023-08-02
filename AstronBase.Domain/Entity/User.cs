
using Microsoft.AspNetCore.Identity;

namespace AstronBase.Domain.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedData { get; set; } = DateTime.Now;
        public List<Request> Requests { get; set; } = new();
        public List<Role> Roles { get; set; } = new();
    }
}