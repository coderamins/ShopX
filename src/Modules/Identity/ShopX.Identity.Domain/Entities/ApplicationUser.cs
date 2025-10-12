using Microsoft.AspNetCore.Identity;

namespace ShopX.Identity.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
