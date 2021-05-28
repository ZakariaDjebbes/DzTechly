using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class OrdersInformationsrelationfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipToAddress_LastName",
                table: "Orders",
                newName: "PersonalInformation_LastName");

            migrationBuilder.RenameColumn(
                name: "ShipToAddress_FirstName",
                table: "Orders",
                newName: "PersonalInformation_FirstName");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalInformation_BirthDate",
                table: "Orders",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PersonalInformation_BirthDate",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PersonalInformation_LastName",
                table: "Orders",
                newName: "ShipToAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "PersonalInformation_FirstName",
                table: "Orders",
                newName: "ShipToAddress_FirstName");
        }
    }
}
