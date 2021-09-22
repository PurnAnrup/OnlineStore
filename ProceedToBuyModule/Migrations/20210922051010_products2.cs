using Microsoft.EntityFrameworkCore.Migrations;

namespace ProceedToBuyModule.Migrations
{
    public partial class products2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerWishLists",
                table: "CustomerWishLists",
                columns: new[] { "CartId", "ProductId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerWishLists",
                table: "CustomerWishLists");
        }
    }
}
