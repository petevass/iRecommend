using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRecommendWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class CollectCollegeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "college_name",
                table: "recommendation_letters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "college_name",
                table: "recommendation_letters");
        }
    }
}
