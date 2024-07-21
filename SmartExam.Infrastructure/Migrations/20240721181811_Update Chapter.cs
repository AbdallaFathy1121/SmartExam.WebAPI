using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartExam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_SubjectId",
                table: "Chapters",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Subjects_SubjectId",
                table: "Chapters",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Subjects_SubjectId",
                table: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_SubjectId",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Chapters");
        }
    }
}
