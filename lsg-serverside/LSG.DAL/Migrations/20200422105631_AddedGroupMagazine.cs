using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedGroupMagazine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_GroupId",
                table: "Items",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Groups_GroupId",
                table: "Items",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Groups_GroupId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_GroupId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Items");
        }
    }
}
