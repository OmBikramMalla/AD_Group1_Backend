using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAppointmentVehicleRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReviews_ServiceAppointments_ServiceAppointmentId",
                table: "ServiceReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReviews_ServiceAppointments_ServiceAppointmentId",
                table: "ServiceReviews",
                column: "ServiceAppointmentId",
                principalTable: "ServiceAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReviews_ServiceAppointments_ServiceAppointmentId",
                table: "ServiceReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReviews_ServiceAppointments_ServiceAppointmentId",
                table: "ServiceReviews",
                column: "ServiceAppointmentId",
                principalTable: "ServiceAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
