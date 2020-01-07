using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedRotationFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rot",
                table: "Vehicles");

            migrationBuilder.AddColumn<float>(
                name: "RotPitch",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "RotRoll",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "RotYaw",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RotPitch",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "RotRoll",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "RotYaw",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "Rot",
                table: "Vehicles",
                nullable: true);
        }
    }
}
