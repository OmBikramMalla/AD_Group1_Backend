using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingAppointmentReviewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceAppointments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "VehicleId",
                table: "ServiceAppointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointments_VehicleId",
                table: "ServiceAppointments",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAppointments_Vehicles_VehicleId",
                table: "ServiceAppointments");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAppointments_VehicleId",
                table: "ServiceAppointments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceAppointments");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "ServiceAppointments");
        }
    }
}
