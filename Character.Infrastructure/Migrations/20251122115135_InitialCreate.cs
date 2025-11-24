using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Character.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MoveSpeed = table.Column<float>(type: "REAL", nullable: false),
                    CanUseSpell = table.Column<bool>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    HitPoint = table.Column<int>(type: "INTEGER", nullable: false),
                    Luck = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestName = table.Column<string>(type: "TEXT", nullable: true),
                    QuestDescription = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterProfileModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdCharacter = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterProfileModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterProfileModel_Characters_IdCharacter",
                        column: x => x.IdCharacter,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mages_Characters_Id",
                        column: x => x.Id,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warriors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warriors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warriors_Characters_Id",
                        column: x => x.Id,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weapons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    WeaponName = table.Column<string>(type: "TEXT", nullable: true),
                    WeaponLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    WeaponDamage = table.Column<float>(type: "REAL", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weapons_Warriors_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Warriors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterProfileModel_IdCharacter",
                table: "CharacterProfileModel",
                column: "IdCharacter",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weapons_OwnerId",
                table: "Weapons",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterProfileModel");

            migrationBuilder.DropTable(
                name: "Mages");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Weapons");

            migrationBuilder.DropTable(
                name: "Warriors");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
