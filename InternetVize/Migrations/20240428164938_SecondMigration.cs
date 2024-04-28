using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetVize.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Rents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RentalProfileId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_VehicleId",
                table: "Rents",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RentalProfileId",
                table: "Addresses",
                column: "RentalProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_RentalProfiles_RentalProfileId",
                table: "Addresses",
                column: "RentalProfileId",
                principalTable: "RentalProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Vehicles_VehicleId",
                table: "Rents",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_RentalProfiles_RentalProfileId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Vehicles_VehicleId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Rents_VehicleId",
                table: "Rents");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_RentalProfileId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "RentalProfileId",
                table: "Addresses");
        }
    }
}
