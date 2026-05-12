namespace WebApplications.Application.DTOs
{
    /// <summary>
    /// DTO used by Admin to register a new Staff member.
    /// </summary>
    public class RegisterStaffDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Phone { get; set; }

        /// <summary>
        /// Role must be "Staff". Defaults to "Staff" so Admin doesn't need to specify it.
        /// </summary>
        public string Role { get; set; } = "Staff";
    }
}
