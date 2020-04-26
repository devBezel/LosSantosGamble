using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class UpdateGroupRanksModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupRanks_Groups_DefaultForGroupId",
                table: "GroupRanks");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultForGroupId",
                table: "GroupRanks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_GroupRanks_Groups_DefaultForGroupId",
                table: "GroupRanks",
                column: "DefaultForGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupRanks_Groups_DefaultForGroupId",
                table: "GroupRanks");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultForGroupId",
                table: "GroupRanks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupRanks_Groups_DefaultForGroupId",
                table: "GroupRanks",
                column: "DefaultForGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
