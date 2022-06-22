using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_okul.DataAccess.Migrations
{
    public partial class AddLectureNamesToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Checked",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LectureNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudenId = table.Column<int>(type: "int", nullable: false),
                    LectureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureNames", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureNames");

            migrationBuilder.DropColumn(
                name: "Checked",
                table: "Lectures");
        }
    }
}
