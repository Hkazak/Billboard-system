using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTarrifsCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_GroupOfTariffs_GroupOfTariffsId",
                table: "Tariffs");

            migrationBuilder.DropIndex(
                name: "IX_Tariffs_GroupOfTariffsId",
                table: "Tariffs");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f75e716-09a4-44f9-8110-c0a7d4ae921b"));

            migrationBuilder.DropColumn(
                name: "GroupOfTariffsId",
                table: "Tariffs");

            migrationBuilder.CreateTable(
                name: "GroupOfTariffsTariff",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TariffsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOfTariffsTariff", x => new { x.GroupsId, x.TariffsId });
                    table.ForeignKey(
                        name: "FK_GroupOfTariffsTariff_GroupOfTariffs_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "GroupOfTariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupOfTariffsTariff_Tariffs_TariffsId",
                        column: x => x.TariffsId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("1e356e57-bd29-40d6-9578-63cb0578625d"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_GroupOfTariffsTariff_TariffsId",
                table: "GroupOfTariffsTariff",
                column: "TariffsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupOfTariffsTariff");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1e356e57-bd29-40d6-9578-63cb0578625d"));

            migrationBuilder.AddColumn<Guid>(
                name: "GroupOfTariffsId",
                table: "Tariffs",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("0f75e716-09a4-44f9-8110-c0a7d4ae921b"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_GroupOfTariffsId",
                table: "Tariffs",
                column: "GroupOfTariffsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_GroupOfTariffs_GroupOfTariffsId",
                table: "Tariffs",
                column: "GroupOfTariffsId",
                principalTable: "GroupOfTariffs",
                principalColumn: "Id");
        }
    }
}
