using Microsoft.AspNetCore.Identity;
using WebApplications.Domain.Models;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<Users> _userManager;

    public AuthRepository(UserManager<Users> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(Users user, string password, string role)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return result;
    }

    public async Task<Users> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null) return null;

        var valid = await _userManager.CheckPasswordAsync(user, password);

        return valid ? user : null;
    }

    public async Task<IList<string>> GetRolesAsync(Users user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}