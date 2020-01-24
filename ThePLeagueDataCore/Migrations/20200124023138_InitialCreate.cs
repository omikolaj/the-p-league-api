using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThePLeagueDataCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    InStock = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeagueImages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Small = table.Column<string>(nullable: true),
                    Medium = table.Column<string>(nullable: true),
                    Big = table.Column<string>(nullable: true),
                    CloudinaryPublicId = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    ResourceType = table.Column<string>(nullable: true),
                    HashTag = table.Column<string>(nullable: true),
                    Delete = table.Column<bool>(nullable: true),
                    OrderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueImages", x => x.Id);
                });

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
                name: "SportTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamSignUpForms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSignUpForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GearImages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Small = table.Column<string>(nullable: true),
                    Medium = table.Column<string>(nullable: true),
                    Big = table.Column<string>(nullable: true),
                    CloudinaryPublicId = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    ResourceType = table.Column<string>(nullable: true),
                    GearItemId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GearImages_GearItems_GearItemId",
                        column: x => x.GearItemId,
                        principalTable: "GearItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GearSizes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GearItemId = table.Column<long>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Available = table.Column<bool>(nullable: false),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GearSizes_GearItems_GearItemId",
                        column: x => x.GearItemId,
                        principalTable: "GearItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    SportTypeID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leagues_SportTypes_SportTypeID",
                        column: x => x.SportTypeID,
                        principalTable: "SportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamsContact",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    PreferredContact = table.Column<int>(nullable: false),
                    TeamSignUpFormId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamsContact_TeamSignUpForms_TeamSignUpFormId",
                        column: x => x.TeamSignUpFormId,
                        principalTable: "TeamSignUpForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeagueSessions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LeagueID = table.Column<string>(nullable: true),
                    ByeWeeks = table.Column<bool>(nullable: false),
                    NumberOfWeeks = table.Column<long>(nullable: true),
                    SessionStart = table.Column<DateTime>(nullable: false),
                    SessionEnd = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueSessions_Leagues_LeagueID",
                        column: x => x.LeagueID,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    LeagueID = table.Column<string>(nullable: true),
                    Selected = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueID",
                        column: x => x.LeagueID,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameDays",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    GamesDay = table.Column<string>(nullable: true),
                    LeagueSessionScheduleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameDays_LeagueSessions_LeagueSessionScheduleId",
                        column: x => x.LeagueSessionScheduleId,
                        principalTable: "LeagueSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<string>(nullable: false),
                    DateTime = table.Column<long>(nullable: false),
                    HomeTeamId = table.Column<string>(nullable: true),
                    AwayTeamId = table.Column<string>(nullable: true),
                    LeagueSessionScheduleId = table.Column<string>(nullable: true),
                    LeagueID = table.Column<string>(nullable: true),
                    TeamId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Leagues_LeagueID",
                        column: x => x.LeagueID,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_LeagueSessions_LeagueSessionScheduleId",
                        column: x => x.LeagueSessionScheduleId,
                        principalTable: "LeagueSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamsSessions",
                columns: table => new
                {
                    TeamId = table.Column<string>(nullable: false),
                    LeagueSessionScheduleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsSessions", x => new { x.TeamId, x.LeagueSessionScheduleId });
                    table.ForeignKey(
                        name: "FK_TeamsSessions_LeagueSessions_LeagueSessionScheduleId",
                        column: x => x.LeagueSessionScheduleId,
                        principalTable: "LeagueSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamsSessions_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTimes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    GameDayId = table.Column<string>(nullable: true),
                    GamesTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTimes_GameDays_GameDayId",
                        column: x => x.GameDayId,
                        principalTable: "GameDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchResults",
                columns: table => new
                {
                    MatchResultId = table.Column<string>(nullable: false),
                    MatchId = table.Column<string>(nullable: true),
                    LeagueId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AwayTeamScore = table.Column<long>(nullable: false),
                    AwayTeamId = table.Column<string>(nullable: true),
                    HomeTeamScore = table.Column<long>(nullable: false),
                    HomeTeamId = table.Column<string>(nullable: true),
                    Score = table.Column<string>(nullable: true),
                    WonTeamName = table.Column<string>(nullable: true),
                    LostTeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResults", x => x.MatchResultId);
                    table.ForeignKey(
                        name: "FK_MatchResults_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "GearItems",
                columns: new[] { "Id", "InStock", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, true, "T-shirt", 25m },
                    { 2L, false, "Pants", 26m },
                    { 3L, true, "Hat", 27m },
                    { 4L, false, "Jorts", 28m },
                    { 5L, true, "Long Sleeve", 29m },
                    { 6L, false, "Shoes", 30m },
                    { 7L, true, "Trousers", 31m },
                    { 8L, false, "Zip-Up", 32m },
                    { 9L, true, "Track Jacket", 33m },
                    { 10L, false, "Cut off", 34m },
                    { 11L, true, "Suprirse", 35m }
                });

            migrationBuilder.InsertData(
                table: "SportTypes",
                columns: new[] { "Id", "Active", "Name" },
                values: new object[,]
                {
                    { "1", true, "Basketball" },
                    { "2", true, "Volleyball" },
                    { "3", true, "Soccer" }
                });

            migrationBuilder.InsertData(
                table: "GearImages",
                columns: new[] { "Id", "Big", "CloudinaryPublicId", "Format", "GearItemId", "Height", "Medium", "Name", "ResourceType", "Size", "Small", "Type", "Url", "Width" },
                values: new object[,]
                {
                    { 11231L, "https://via.placeholder.com/300.png/09f/fff", null, null, 1L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11240L, "https://via.placeholder.com/300.png/09f/fff", null, null, 10L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11234L, "https://via.placeholder.com/300.png/09f/fff", null, null, 4L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2325L, "https://via.placeholder.com/300.png/09f/fff", null, null, 4L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11235L, "https://via.placeholder.com/300.png/09f/fff", null, null, 5L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2326L, "https://via.placeholder.com/300.png/09f/fff", null, null, 5L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34327L, "https://via.placeholder.com/300.png/09f/fff", null, null, 5L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34331L, "https://via.placeholder.com/300.png/09f/fff", null, null, 9L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2330L, "https://via.placeholder.com/300.png/09f/fff", null, null, 9L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2331L, "https://via.placeholder.com/300.png/09f/fff", null, null, 10L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11239L, "https://via.placeholder.com/300.png/09f/fff", null, null, 9L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2327L, "https://via.placeholder.com/300.png/09f/fff", null, null, 6L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34328L, "https://via.placeholder.com/300.png/09f/fff", null, null, 6L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34330L, "https://via.placeholder.com/300.png/09f/fff", null, null, 8L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2329L, "https://via.placeholder.com/300.png/09f/fff", null, null, 8L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11238L, "https://via.placeholder.com/300.png/09f/fff", null, null, 8L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11237L, "https://via.placeholder.com/300.png/09f/fff", null, null, 7L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2328L, "https://via.placeholder.com/300.png/09f/fff", null, null, 7L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34329L, "https://via.placeholder.com/300.png/09f/fff", null, null, 7L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11236L, "https://via.placeholder.com/300.png/09f/fff", null, null, 6L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34332L, "https://via.placeholder.com/300.png/09f/fff", null, null, 10L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34326L, "https://via.placeholder.com/300.png/09f/fff", null, null, 4L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34324L, "https://via.placeholder.com/300.png/09f/fff", null, null, 2L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2323L, "https://via.placeholder.com/300.png/09f/fff", null, null, 2L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34325L, "https://via.placeholder.com/300.png/09f/fff", null, null, 3L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2324L, "https://via.placeholder.com/300.png/09f/fff", null, null, 3L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11233L, "https://via.placeholder.com/300.png/09f/fff", null, null, 3L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2322L, "https://via.placeholder.com/300.png/09f/fff", null, null, 1L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34323L, "https://via.placeholder.com/300.png/09f/fff", null, null, 1L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 34333L, "https://via.placeholder.com/300.png/09f/fff", null, null, 11L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwaw", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 2332L, "https://via.placeholder.com/300.png/09f/fff", null, null, 11L, 0, "https://via.placeholder.com/300.png/09f/fff", "wowwee", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11232L, "https://via.placeholder.com/300.png/09f/fff", null, null, 2L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 },
                    { 11241L, "https://via.placeholder.com/300.png/09f/fff", null, null, 11L, 0, "https://via.placeholder.com/300.png/09f/fff", "wow", null, 19392L, "https://via.placeholder.com/300.png/09f/fff", "png", "https://via.placeholder.com/300.png/09f/fff", 0 }
                });

            migrationBuilder.InsertData(
                table: "GearSizes",
                columns: new[] { "Id", "Available", "Color", "GearItemId", "Size" },
                values: new object[,]
                {
                    { 112L, true, "warn", 11L, 4 },
                    { 1344L, false, "warn", 11L, 5 },
                    { 54L, true, "warn", 11L, 6 },
                    { 1341L, false, "warn", 8L, 5 },
                    { 109L, true, "warn", 8L, 4 },
                    { 45688L, false, "warn", 11L, 3 },
                    { 9109L, false, "warn", 11L, 2 },
                    { 85853L, true, "warn", 11L, 1 },
                    { 85849L, true, "warn", 7L, 1 },
                    { 51L, true, "warn", 8L, 6 },
                    { 45685L, false, "warn", 8L, 3 },
                    { 1343L, false, "warn", 10L, 5 },
                    { 85850L, true, "warn", 8L, 1 },
                    { 53L, true, "warn", 10L, 6 },
                    { 85852L, true, "warn", 10L, 1 },
                    { 110L, true, "warn", 9L, 4 },
                    { 1342L, false, "warn", 9L, 5 },
                    { 52L, true, "warn", 9L, 6 },
                    { 45686L, false, "warn", 9L, 3 },
                    { 9107L, false, "warn", 9L, 2 },
                    { 85851L, true, "warn", 9L, 1 },
                    { 9105L, false, "warn", 7L, 2 },
                    { 9108L, false, "warn", 10L, 2 },
                    { 45687L, false, "warn", 10L, 3 },
                    { 111L, true, "warn", 10L, 4 },
                    { 9106L, false, "warn", 8L, 2 },
                    { 45684L, false, "warn", 7L, 3 },
                    { 9104L, false, "warn", 6L, 2 },
                    { 1340L, false, "warn", 7L, 5 },
                    { 50L, true, "warn", 7L, 6 },
                    { 46L, true, "warn", 3L, 6 },
                    { 1336L, false, "warn", 3L, 5 },
                    { 104L, true, "warn", 3L, 4 },
                    { 85844L, true, "warn", 2L, 1 },
                    { 9100L, false, "warn", 2L, 2 },
                    { 45679L, false, "warn", 2L, 3 },
                    { 9101L, false, "warn", 3L, 2 },
                    { 45L, true, "warn", 2L, 6 },
                    { 103L, true, "warn", 2L, 4 },
                    { 85843L, true, "warn", 1L, 1 },
                    { 9099L, false, "warn", 1L, 2 },
                    { 45678L, false, "warn", 1L, 3 },
                    { 44L, true, "warn", 1L, 6 },
                    { 1334L, false, "warn", 1L, 5 },
                    { 102L, true, "warn", 1L, 4 },
                    { 1335L, false, "warn", 2L, 5 },
                    { 85845L, true, "warn", 3L, 1 },
                    { 45680L, false, "warn", 3L, 3 },
                    { 1337L, false, "warn", 4L, 5 },
                    { 108L, true, "warn", 7L, 4 },
                    { 85848L, true, "warn", 6L, 1 },
                    { 45683L, false, "warn", 6L, 3 },
                    { 49L, true, "warn", 6L, 6 },
                    { 105L, true, "warn", 4L, 4 },
                    { 107L, true, "warn", 6L, 4 },
                    { 85847L, true, "warn", 5L, 1 },
                    { 9103L, false, "warn", 5L, 2 },
                    { 1339L, false, "warn", 6L, 5 },
                    { 48L, true, "warn", 5L, 6 },
                    { 1338L, false, "warn", 5L, 5 },
                    { 106L, true, "warn", 5L, 4 },
                    { 85846L, true, "warn", 4L, 1 },
                    { 9102L, false, "warn", 4L, 2 },
                    { 45681L, false, "warn", 4L, 3 },
                    { 47L, true, "warn", 4L, 6 },
                    { 45682L, false, "warn", 5L, 3 }
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Active", "Name", "Selected", "SportTypeID", "Type" },
                values: new object[,]
                {
                    { "6", true, "Saturday", false, "2", "Volleyball" },
                    { "1", true, "Monday", false, "1", "Basketball" },
                    { "2", true, "Tuesday", false, "1", "Basketball" },
                    { "3", true, "Wednesday", false, "1", "Basketball" },
                    { "4", true, "Thursday", false, "2", "Volleyball" },
                    { "5", true, "Friday", false, "2", "Volleyball" },
                    { "7", true, "Sunday", false, "3", "Soccer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GameDays_LeagueSessionScheduleId",
                table: "GameDays",
                column: "LeagueSessionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTimes_GameDayId",
                table: "GameTimes",
                column: "GameDayId");

            migrationBuilder.CreateIndex(
                name: "IX_GearImages_GearItemId",
                table: "GearImages",
                column: "GearItemId");

            migrationBuilder.CreateIndex(
                name: "IX_GearSizes_GearItemId",
                table: "GearSizes",
                column: "GearItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SportTypeID",
                table: "Leagues",
                column: "SportTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueSessions_LeagueID",
                table: "LeagueSessions",
                column: "LeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueID",
                table: "Matches",
                column: "LeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueSessionScheduleId",
                table: "Matches",
                column: "LeagueSessionScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TeamId",
                table: "Matches",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResults_MatchId",
                table: "MatchResults",
                column: "MatchId",
                unique: true,
                filter: "[MatchId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrderContacts_PreOrderId",
                table: "PreOrderContacts",
                column: "PreOrderId",
                unique: true,
                filter: "[PreOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueID",
                table: "Teams",
                column: "LeagueID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsContact_TeamSignUpFormId",
                table: "TeamsContact",
                column: "TeamSignUpFormId",
                unique: true,
                filter: "[TeamSignUpFormId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsSessions_LeagueSessionScheduleId",
                table: "TeamsSessions",
                column: "LeagueSessionScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GameTimes");

            migrationBuilder.DropTable(
                name: "GearImages");

            migrationBuilder.DropTable(
                name: "GearSizes");

            migrationBuilder.DropTable(
                name: "LeagueImages");

            migrationBuilder.DropTable(
                name: "MatchResults");

            migrationBuilder.DropTable(
                name: "PreOrderContacts");

            migrationBuilder.DropTable(
                name: "TeamsContact");

            migrationBuilder.DropTable(
                name: "TeamsSessions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GameDays");

            migrationBuilder.DropTable(
                name: "GearItems");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "PreOrders");

            migrationBuilder.DropTable(
                name: "TeamSignUpForms");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "LeagueSessions");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "SportTypes");
        }
    }
}
