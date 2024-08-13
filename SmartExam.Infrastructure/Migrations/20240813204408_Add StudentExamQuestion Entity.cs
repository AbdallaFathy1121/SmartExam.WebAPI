using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartExam.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentExamQuestionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentExamQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    StudentExamId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExamQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExamQuestions_StudentExams_StudentExamId",
                        column: x => x.StudentExamId,
                        principalTable: "StudentExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamQuestions_QuestionId",
                table: "StudentExamQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamQuestions_StudentExamId",
                table: "StudentExamQuestions",
                column: "StudentExamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExamQuestions");
        }
    }
}
