using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetVize.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Addresses_AddressId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_RentalProfiles_RentalProfileId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                nullable: true);

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Vehicles_VehicleId",
                table: "Rents",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Addresses_AddressId",
                table: "Vehicles",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_RentalProfiles_RentalProfileId",
                table: "Vehicles",
                column: "RentalProfileId",
                principalTable: "RentalProfiles",
                principalColumn: "Id");
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

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Addresses_AddressId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_RentalProfiles_RentalProfileId",
                table: "Vehicles");

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

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Addresses_AddressId",
                table: "Vehicles",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_RentalProfiles_RentalProfileId",
                table: "Vehicles",
                column: "RentalProfileId",
                principalTable: "RentalProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
