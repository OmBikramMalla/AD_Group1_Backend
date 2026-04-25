using Microsoft.AspNetCore.Identity;
using WebApplications.Domain.Models;

public interface IAuthRepository
{
    Task<IdentityResult> RegisterAsync(Users user, string password, string role);
    Task<Users> LoginAsync(string email, string password);
    Task<IList<string>> GetRolesAsync(Users user);
}