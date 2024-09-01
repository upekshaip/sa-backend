using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_SellerIdUserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_SellerIdUserId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "SellerIdUserId",
                table: "Auctions",
                newName: "SellerId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Auctions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_UserId",
                table: "Auctions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_UserId",
                table: "Auctions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_UserId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_UserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Auctions",
                newName: "SellerIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_SellerIdUserId",
                table: "Auctions",
                column: "SellerIdUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_SellerIdUserId",
                table: "Auctions",
                column: "SellerIdUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
