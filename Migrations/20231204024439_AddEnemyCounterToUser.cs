using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorServerApp.Migrations
{
    public partial class AddEnemyCounterToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnemyCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TowerBought",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnemyCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TowerBought",
                table: "AspNetUsers");
        }
    }
}
