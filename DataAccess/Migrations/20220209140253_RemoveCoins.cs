using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class RemoveCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Coin_CoinId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Coin");

            migrationBuilder.DropIndex(
                name: "IX_Order_CoinId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CoinId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "Coin",
                table: "Order",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coin",
                table: "Order");

            migrationBuilder.AddColumn<Guid>(
                name: "CoinId",
                table: "Order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Coin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Identifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CoinId",
                table: "Order",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Coin_Name_Identifier",
                table: "Coin",
                columns: new[] { "Name", "Identifier" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Coin_CoinId",
                table: "Order",
                column: "CoinId",
                principalTable: "Coin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
