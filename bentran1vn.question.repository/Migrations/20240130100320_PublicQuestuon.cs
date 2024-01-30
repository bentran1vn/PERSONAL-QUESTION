using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bentran1vn.question.repository.Migrations
{
    public partial class PublicQuestuon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserQuestionId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicQuestion_UserQuestions_UserQuestionId",
                        column: x => x.UserQuestionId,
                        principalTable: "UserQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicQuestion_UserQuestionId",
                table: "PublicQuestion",
                column: "UserQuestionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicQuestion");
        }
    }
}
