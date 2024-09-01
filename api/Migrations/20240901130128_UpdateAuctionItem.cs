using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuctionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItems_Auctions_AuctionId",
                table: "AuctionItems");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "AuctionItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItems_Auctions_AuctionId",
                table: "AuctionItems",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItems_Auctions_AuctionId",
                table: "AuctionItems");

            migrationBuilder.AlterColumn<int>(
                name: "AuctionId",
                table: "AuctionItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItems_Auctions_AuctionId",
                table: "AuctionItems",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "AuctionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
