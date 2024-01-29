using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bentran1vn.question.repository.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_UserQuestion_UserQuestionId",
                table: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "UserQuestion");

            migrationBuilder.CreateTable(
                name: "UserQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuestions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_UserId",
                table: "UserQuestions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_UserQuestions_UserQuestionId",
                table: "QuestionAnswers",
                column: "UserQuestionId",
                principalTable: "UserQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionAnswers_UserQuestions_UserQuestionId",
                table: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "UserQuestions");

            migrationBuilder.CreateTable(
                name: "UserQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuestion_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestion_UserId",
                table: "UserQuestion",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionAnswers_UserQuestion_UserQuestionId",
                table: "QuestionAnswers",
                column: "UserQuestionId",
                principalTable: "UserQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
