using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class Datadesignflawfixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Products_ProductId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdditionalInfo_AdditionalInfoCategories_AdditionalinfoCategoryId",
                table: "ProductAdditionalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdditionalInfo_TechnicalSheets_TechnicalSheetId",
                table: "ProductAdditionalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_TechnicalSheets_TechnicalSheetId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_TechnicalSheetId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductAdditionalInfo_AdditionalinfoCategoryId",
                table: "ProductAdditionalInfo");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProductId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TechnicalSheetId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AdditionalinfoCategoryId",
                table: "ProductAdditionalInfo");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "TechnicalSheets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalSheetId",
                table: "ProductAdditionalInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalInfoCategoryId",
                table: "AdditionalInfoNames",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppUserProduct",
                columns: table => new
                {
                    WaitingListId = table.Column<string>(type: "TEXT", nullable: false),
                    WaitingProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProduct", x => new { x.WaitingListId, x.WaitingProductsId });
                    table.ForeignKey(
                        name: "FK_AppUserProduct_AspNetUsers_WaitingListId",
                        column: x => x.WaitingListId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserProduct_Products_WaitingProductsId",
                        column: x => x.WaitingProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalSheets_ProductId",
                table: "TechnicalSheets",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfoNames_AdditionalInfoCategoryId",
                table: "AdditionalInfoNames",
                column: "AdditionalInfoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserProduct_WaitingProductsId",
                table: "AppUserProduct",
                column: "WaitingProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalInfoNames_AdditionalInfoCategories_AdditionalInfoCategoryId",
                table: "AdditionalInfoNames",
                column: "AdditionalInfoCategoryId",
                principalTable: "AdditionalInfoCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdditionalInfo_TechnicalSheets_TechnicalSheetId",
                table: "ProductAdditionalInfo",
                column: "TechnicalSheetId",
                principalTable: "TechnicalSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalSheets_Products_ProductId",
                table: "TechnicalSheets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalInfoNames_AdditionalInfoCategories_AdditionalInfoCategoryId",
                table: "AdditionalInfoNames");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAdditionalInfo_TechnicalSheets_TechnicalSheetId",
                table: "ProductAdditionalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalSheets_Products_ProductId",
                table: "TechnicalSheets");

            migrationBuilder.DropTable(
                name: "AppUserProduct");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalSheets_ProductId",
                table: "TechnicalSheets");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalInfoNames_AdditionalInfoCategoryId",
                table: "AdditionalInfoNames");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "TechnicalSheets");

            migrationBuilder.DropColumn(
                name: "AdditionalInfoCategoryId",
                table: "AdditionalInfoNames");

            migrationBuilder.AddColumn<int>(
                name: "TechnicalSheetId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TechnicalSheetId",
                table: "ProductAdditionalInfo",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalinfoCategoryId",
                table: "ProductAdditionalInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_TechnicalSheetId",
                table: "Products",
                column: "TechnicalSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalInfo_AdditionalinfoCategoryId",
                table: "ProductAdditionalInfo",
                column: "AdditionalinfoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProductId",
                table: "AspNetUsers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Products_ProductId",
                table: "AspNetUsers",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdditionalInfo_AdditionalInfoCategories_AdditionalinfoCategoryId",
                table: "ProductAdditionalInfo",
                column: "AdditionalinfoCategoryId",
                principalTable: "AdditionalInfoCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAdditionalInfo_TechnicalSheets_TechnicalSheetId",
                table: "ProductAdditionalInfo",
                column: "TechnicalSheetId",
                principalTable: "TechnicalSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_TechnicalSheets_TechnicalSheetId",
                table: "Products",
                column: "TechnicalSheetId",
                principalTable: "TechnicalSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
