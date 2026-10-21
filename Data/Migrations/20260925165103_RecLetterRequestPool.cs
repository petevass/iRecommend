using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRecommendWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecLetterRequestPool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "letter_pools",
                columns: table => new
                {
                    poolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pool_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    request_ids = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_letter_pools", x => x.poolId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_letter_pools_poolId",
                table: "letter_pools",
                column: "poolId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "letter_pools");
        }
    }
}
