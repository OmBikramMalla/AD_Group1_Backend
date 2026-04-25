public interface IAuthService
{
    Task<string> RegisterStudentAsync(RegisterStudentDto dto);
    Task<string> RegisterInstructorAsync(RegisterInstructorDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}