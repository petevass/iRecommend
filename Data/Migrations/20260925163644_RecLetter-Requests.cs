using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iRecommendWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class RecLetterRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recommendation_letters",
                columns: table => new
                {
                    letterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    teacher_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    submision_method = table.Column<int>(type: "int", nullable: false),
                    extra_details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    current_progress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendation_letters", x => x.letterId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recommendation_letters_letterId",
                table: "recommendation_letters",
                column: "letterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recommendation_letters");
        }
    }
}
