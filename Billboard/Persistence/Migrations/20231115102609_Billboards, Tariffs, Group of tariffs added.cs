using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BillboardsTariffsGroupoftariffsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1043923-3e8d-436a-a3f7-07a466f300f7"));

            migrationBuilder.CreateTable(
                name: "ArchiveStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillboardSurfaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Surface = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillboardSurfaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillboardType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillboardType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupOfTariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ArchiveStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupOfTariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupOfTariffs_ArchiveStatus_ArchiveStatusId",
                        column: x => x.ArchiveStatusId,
                        principalTable: "ArchiveStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Billboards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Address = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    ArchiveStatusId = table.Column<int>(type: "integer", nullable: false),
                    BillboardSurfaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Width = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Height = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Penalty = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    GroupOfTariffsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billboards_ArchiveStatus_ArchiveStatusId",
                        column: x => x.ArchiveStatusId,
                        principalTable: "ArchiveStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Billboards_BillboardSurfaces_BillboardSurfaceId",
                        column: x => x.BillboardSurfaceId,
                        principalTable: "BillboardSurfaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Billboards_BillboardType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BillboardType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Billboards_GroupOfTariffs_GroupOfTariffsId",
                        column: x => x.GroupOfTariffsId,
                        principalTable: "GroupOfTariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    ArchiveStatusId = table.Column<int>(type: "integer", nullable: false),
                    GroupOfTariffsId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tariffs_ArchiveStatus_ArchiveStatusId",
                        column: x => x.ArchiveStatusId,
                        principalTable: "ArchiveStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tariffs_GroupOfTariffs_GroupOfTariffsId",
                        column: x => x.GroupOfTariffsId,
                        principalTable: "GroupOfTariffs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    BillboardId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_Billboards_BillboardId",
                        column: x => x.BillboardId,
                        principalTable: "Billboards",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("3ad92bd3-975b-4dfc-9690-ac53bdc280a8"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Billboards_ArchiveStatusId",
                table: "Billboards",
                column: "ArchiveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Billboards_BillboardSurfaceId",
                table: "Billboards",
                column: "BillboardSurfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Billboards_GroupOfTariffsId",
                table: "Billboards",
                column: "GroupOfTariffsId");

            migrationBuilder.CreateIndex(
                name: "IX_Billboards_TypeId",
                table: "Billboards",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupOfTariffs_ArchiveStatusId",
                table: "GroupOfTariffs",
                column: "ArchiveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Picture_BillboardId",
                table: "Picture",
                column: "BillboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_ArchiveStatusId",
                table: "Tariffs",
                column: "ArchiveStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_GroupOfTariffsId",
                table: "Tariffs",
                column: "GroupOfTariffsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "Billboards");

            migrationBuilder.DropTable(
                name: "BillboardSurfaces");

            migrationBuilder.DropTable(
                name: "BillboardType");

            migrationBuilder.DropTable(
                name: "GroupOfTariffs");

            migrationBuilder.DropTable(
                name: "ArchiveStatus");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3ad92bd3-975b-4dfc-9690-ac53bdc280a8"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("f1043923-3e8d-436a-a3f7-07a466f300f7"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });
        }
    }
}
