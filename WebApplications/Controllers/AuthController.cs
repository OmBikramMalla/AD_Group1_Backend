using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    // ✅ POST: api/auth/register-student
    [HttpPost("register-student")]
    public async Task<IActionResult> RegisterStudent(RegisterStudentDto dto)
    {
        var result = await _service.RegisterStudentAsync(dto);
        return Ok(result);
    }

    // ✅ POST: api/auth/register-instructor
    [HttpPost("register-instructor")]
    public async Task<IActionResult> RegisterInstructor(RegisterInstructorDto dto)
    {
        var result = await _service.RegisterInstructorAsync(dto);
        return Ok(result);
    }

    // ✅ POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _service.LoginAsync(dto);

        if (result == null)
            return Unauthorized("Invalid email or password");

        return Ok(result);
    }
}