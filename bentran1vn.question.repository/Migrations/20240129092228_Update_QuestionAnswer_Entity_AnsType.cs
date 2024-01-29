using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bentran1vn.question.repository.Migrations
{
    public partial class Update_QuestionAnswer_Entity_AnsType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerType",
                table: "QuestionAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerType",
                table: "QuestionAnswers");
        }
    }
}
