using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class ChangeSenderForGetter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderNumber",
                table: "SmartphoneMessages",
                newName: "GetterNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GetterNumber",
                table: "SmartphoneMessages",
                newName: "SenderNumber");
        }
    }
}
