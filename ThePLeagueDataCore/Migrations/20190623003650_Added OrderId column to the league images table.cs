using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePLeagueDataCore.Migrations
{
    public partial class AddedOrderIdcolumntotheleagueimagestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "LeagueImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "LeagueImages");
        }
    }
}
