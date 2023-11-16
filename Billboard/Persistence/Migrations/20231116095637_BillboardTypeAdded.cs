using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BillboardTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billboards_ArchiveStatus_ArchiveStatusId",
                table: "Billboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Billboards_BillboardType_TypeId",
                table: "Billboards");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOfTariffs_ArchiveStatus_ArchiveStatusId",
                table: "GroupOfTariffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_ArchiveStatus_ArchiveStatusId",
                table: "Tariffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillboardType",
                table: "BillboardType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchiveStatus",
                table: "ArchiveStatus");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5e35a183-46c7-4325-816e-85be677ee13a"));

            migrationBuilder.RenameTable(
                name: "BillboardType",
                newName: "BillboardTypes");

            migrationBuilder.RenameTable(
                name: "ArchiveStatus",
                newName: "ArchiveStatusEnumerable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillboardTypes",
                table: "BillboardTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchiveStatusEnumerable",
                table: "ArchiveStatusEnumerable",
                column: "Id");

            migrationBuilder.InsertData(
                table: "BillboardTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 0, "SingleSide" },
                    { 1, "DoubleSide" },
                    { 2, "TripleSide" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("d513dcc7-8ba7-4736-9a21-f033a3d5c760"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Billboards_ArchiveStatusEnumerable_ArchiveStatusId",
                table: "Billboards",
                column: "ArchiveStatusId",
                principalTable: "ArchiveStatusEnumerable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Billboards_BillboardTypes_TypeId",
                table: "Billboards",
                column: "TypeId",
                principalTable: "BillboardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOfTariffs_ArchiveStatusEnumerable_ArchiveStatusId",
                table: "GroupOfTariffs",
                column: "ArchiveStatusId",
                principalTable: "ArchiveStatusEnumerable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_ArchiveStatusEnumerable_ArchiveStatusId",
                table: "Tariffs",
                column: "ArchiveStatusId",
                principalTable: "ArchiveStatusEnumerable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Billboards_ArchiveStatusEnumerable_ArchiveStatusId",
                table: "Billboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Billboards_BillboardTypes_TypeId",
                table: "Billboards");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupOfTariffs_ArchiveStatusEnumerable_ArchiveStatusId",
                table: "GroupOfTariffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_ArchiveStatusEnumerable_ArchiveStatusId",
                table: "Tariffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillboardTypes",
                table: "BillboardTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchiveStatusEnumerable",
                table: "ArchiveStatusEnumerable");

            migrationBuilder.DeleteData(
                table: "BillboardTypes",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "BillboardTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BillboardTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d513dcc7-8ba7-4736-9a21-f033a3d5c760"));

            migrationBuilder.RenameTable(
                name: "BillboardTypes",
                newName: "BillboardType");

            migrationBuilder.RenameTable(
                name: "ArchiveStatusEnumerable",
                newName: "ArchiveStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillboardType",
                table: "BillboardType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchiveStatus",
                table: "ArchiveStatus",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("5e35a183-46c7-4325-816e-85be677ee13a"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Billboards_ArchiveStatus_ArchiveStatusId",
                table: "Billboards",
                column: "ArchiveStatusId",
                principalTable: "ArchiveStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Billboards_BillboardType_TypeId",
                table: "Billboards",
                column: "TypeId",
                principalTable: "BillboardType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupOfTariffs_ArchiveStatus_ArchiveStatusId",
                table: "GroupOfTariffs",
                column: "ArchiveStatusId",
                principalTable: "ArchiveStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_ArchiveStatus_ArchiveStatusId",
                table: "Tariffs",
                column: "ArchiveStatusId",
                principalTable: "ArchiveStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
