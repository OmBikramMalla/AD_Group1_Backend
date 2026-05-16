using Microsoft.AspNetCore.Identity;

namespace WebApplications.Domain.Models
{
    public class Users : IdentityUser<long>
    {
        public string FullName { get; set; } = string.Empty;
    }
}