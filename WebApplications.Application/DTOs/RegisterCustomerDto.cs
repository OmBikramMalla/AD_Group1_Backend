namespace WebApplications.Application.DTOs
{
	public class RegisterCustomerDto
	{
		public string FullName { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;

		public string VehicleNumber { get; set; } = string.Empty;
		public string VehicleModel { get; set; } = string.Empty;
		public string VehicleBrand { get; set; } = string.Empty;
	}
}