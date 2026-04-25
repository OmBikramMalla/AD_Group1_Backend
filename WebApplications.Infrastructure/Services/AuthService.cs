using WebApplications.Domain.Models;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repo;
    private readonly JwtHelper _jwt;

    public AuthService(IAuthRepository repo, JwtHelper jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    public async Task<string> RegisterStudentAsync(RegisterStudentDto dto)
    {
        var user = new Users
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _repo.RegisterAsync(user, dto.Password, "Student");

        return result.Succeeded ? "Student Registered Successfully" : "Registration Failed";
    }

    public async Task<string> RegisterInstructorAsync(RegisterInstructorDto dto)
    {
        var user = new Users
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _repo.RegisterAsync(user, dto.Password, "Instructor");

        return result.Succeeded ? "Instructor Registered Successfully" : "Registration Failed";
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _repo.LoginAsync(dto.Email, dto.Password);

        if (user == null) return null;

        var roles = await _repo.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        var token = _jwt.GenerateToken(user, role);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Role = role
        };
    }
}