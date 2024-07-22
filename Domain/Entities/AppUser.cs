using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? Bio { get; set; }

        public List<Order> Orders { get; set; }
    }
}
