using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRecommendWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class CollectPoolId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "pool_id",
                table: "recommendation_letters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pool_id",
                table: "recommendation_letters");
        }
    }
}
