using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AreaServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "schm_areaservices");

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "schm_areaservices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LogoUrl = table.Column<string>(type: "character varying(550)", maxLength: 550, nullable: true),
                    DateCreation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "schm_areaservices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Firstname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Lastname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PhotoUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Admin = table.Column<int>(type: "integer", nullable: false),
                    AccountStatus = table.Column<int>(type: "integer", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    GoogleStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionArea",
                schema: "schm_areaservices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionArea_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "schm_areaservices",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReactionArea",
                schema: "schm_areaservices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReactionArea_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "schm_areaservices",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSites",
                schema: "schm_areaservices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserKey = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ServicesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSites_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalSchema: "schm_areaservices",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSites_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "schm_areaservices",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserArea",
                schema: "schm_areaservices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StatusArea = table.Column<int>(type: "integer", nullable: false),
                    StatusInfo = table.Column<string>(type: "text", maxLength: 2147483647, nullable: false),
                    IsActive = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ActionAreaId = table.Column<int>(type: "integer", nullable: false),
                    ParamAction = table.Column<string>(type: "text", nullable: false),
                    InfoAction = table.Column<string>(type: "text", nullable: false),
                    ReactionAreaId = table.Column<int>(type: "integer", nullable: false),
                    ParamReaction = table.Column<string>(type: "text", nullable: false),
                    InfoReaction = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArea_ActionArea_ActionAreaId",
                        column: x => x.ActionAreaId,
                        principalSchema: "schm_areaservices",
                        principalTable: "ActionArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArea_ReactionArea_ReactionAreaId",
                        column: x => x.ReactionAreaId,
                        principalSchema: "schm_areaservices",
                        principalTable: "ReactionArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArea_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "schm_areaservices",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionArea_ServiceId",
                schema: "schm_areaservices",
                table: "ActionArea",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionArea_ServiceId",
                schema: "schm_areaservices",
                table: "ReactionArea",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AccountStatus",
                schema: "schm_areaservices",
                table: "User",
                column: "AccountStatus");

            migrationBuilder.CreateIndex(
                name: "IX_User_Admin",
                schema: "schm_areaservices",
                table: "User",
                column: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "schm_areaservices",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_ActionAreaId",
                schema: "schm_areaservices",
                table: "UserArea",
                column: "ActionAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_ReactionAreaId",
                schema: "schm_areaservices",
                table: "UserArea",
                column: "ReactionAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_StatusArea",
                schema: "schm_areaservices",
                table: "UserArea",
                column: "StatusArea");

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_StatusInfo",
                schema: "schm_areaservices",
                table: "UserArea",
                column: "StatusInfo");

            migrationBuilder.CreateIndex(
                name: "IX_UserArea_UserId",
                schema: "schm_areaservices",
                table: "UserArea",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSites_ServicesId",
                schema: "schm_areaservices",
                table: "UserSites",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSites_UserId",
                schema: "schm_areaservices",
                table: "UserSites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserArea",
                schema: "schm_areaservices");

            migrationBuilder.DropTable(
                name: "UserSites",
                schema: "schm_areaservices");

            migrationBuilder.DropTable(
                name: "ActionArea",
                schema: "schm_areaservices");

            migrationBuilder.DropTable(
                name: "ReactionArea",
                schema: "schm_areaservices");

            migrationBuilder.DropTable(
                name: "User",
                schema: "schm_areaservices");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "schm_areaservices");
        }
    }
}
