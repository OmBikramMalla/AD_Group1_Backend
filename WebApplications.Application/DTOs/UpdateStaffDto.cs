namespace WebApplications.Application.DTOs
{
    /// <summary>
    /// DTO used by Admin to update an existing staff member's details or role.
    /// </summary>
    public class UpdateStaffDto
    {
        public string FullName { get; set; } = string.Empty;
        public string? Phone { get; set; }

        /// <summary>
        /// Allowed values: "Staff", "Admin". Admin can promote/demote roles.
        /// </summary>
        public string Role { get; set; } = "Staff";
    }
}
