using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkReviewToServiceAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.AddColumn<long>(
                name: "ServiceAppointmentId",
                table: "ServiceReviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReviews_ServiceAppointmentId",
                table: "ServiceReviews",
                column: "ServiceAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReviews_ServiceAppointments_ServiceAppointmentId",
                table: "ServiceReviews",
                column: "ServiceAppointmentId",
                principalTable: "ServiceAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReviews_ServiceAppointments_ServiceAppointmentId",
                table: "ServiceReviews");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReviews_ServiceAppointmentId",
                table: "ServiceReviews");

            migrationBuilder.DropColumn(
                name: "ServiceAppointmentId",
                table: "ServiceReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
