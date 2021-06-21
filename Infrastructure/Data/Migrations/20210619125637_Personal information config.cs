using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class Personalinformationconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformation_AspNetUsers_AppUserId",
                table: "PersonalInformation");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformation_AspNetUsers_AppUserId",
                table: "PersonalInformation",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformation_AspNetUsers_AppUserId",
                table: "PersonalInformation");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformation_AspNetUsers_AppUserId",
                table: "PersonalInformation",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
