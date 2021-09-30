using Microsoft.EntityFrameworkCore.Migrations;

namespace ProceedToBuyModule.Migrations
{
    public partial class gsgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerWishLists",
                table: "CustomerWishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerWishLists",
                table: "CustomerWishLists",
                columns: new[] { "Id", "ProductId", "DateAddedToWishList" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "Id", "ProductId", "DeliveryDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerWishLists",
                table: "CustomerWishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerWishLists",
                table: "CustomerWishLists",
                columns: new[] { "Id", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "Id", "ProductId" });
        }
    }
}
