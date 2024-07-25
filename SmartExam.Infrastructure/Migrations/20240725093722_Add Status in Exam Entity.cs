using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartExam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusinExamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Exams",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Exams");
        }
    }
}
