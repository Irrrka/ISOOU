using Microsoft.EntityFrameworkCore.Migrations;

namespace ISOOU.Data.Migrations
{
    public partial class AdmissionSchoolId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdmissionSchoolId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionSchoolId",
                table: "AspNetUsers");
        }
    }
}
