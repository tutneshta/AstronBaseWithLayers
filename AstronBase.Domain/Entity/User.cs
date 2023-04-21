
using Microsoft.AspNetCore.Identity;

namespace AstronBase.Domain.Entity
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}