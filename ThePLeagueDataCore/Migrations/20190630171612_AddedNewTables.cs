using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePLeagueDataCore.Migrations
{
    public partial class AddedNewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredContact",
                table: "TeamsContact",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PreOrders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GearItemId = table.Column<long>(nullable: true),
                    Quantity = table.Column<long>(nullable: true),
                    Size = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreOrderContacts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    PreferredContact = table.Column<int>(nullable: false),
                    PreOrderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreOrderContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreOrderContacts_PreOrders_PreOrderId",
                        column: x => x.PreOrderId,
                        principalTable: "PreOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreOrderContacts_PreOrderId",
                table: "PreOrderContacts",
                column: "PreOrderId",
                unique: true,
                filter: "[PreOrderId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreOrderContacts");

            migrationBuilder.DropTable(
                name: "PreOrders");

            migrationBuilder.DropColumn(
                name: "PreferredContact",
                table: "TeamsContact");
        }
    }
}
