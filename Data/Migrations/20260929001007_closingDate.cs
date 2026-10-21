using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRecommendWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class closingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "pool_closing_date",
                table: "letter_pools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pool_closing_date",
                table: "letter_pools");
        }
    }
}
