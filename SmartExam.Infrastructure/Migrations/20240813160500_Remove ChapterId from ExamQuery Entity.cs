using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartExam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChapterIdfromExamQueryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQueries_Chapters_ChapterId",
                table: "ExamQueries");

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "ExamQueries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQueries_Chapters_ChapterId",
                table: "ExamQueries",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQueries_Chapters_ChapterId",
                table: "ExamQueries");

            migrationBuilder.AlterColumn<int>(
                name: "ChapterId",
                table: "ExamQueries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQueries_Chapters_ChapterId",
                table: "ExamQueries",
                column: "ChapterId",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
