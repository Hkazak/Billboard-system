using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BillboardDescriptionLengthChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c792e7cb-8dbf-4177-b2b1-ad60b68f2307"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Billboards",
                type: "character varying(8192)",
                maxLength: 8192,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("844db647-ff5c-4d29-9e6f-3f65f6c12059"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("844db647-ff5c-4d29-9e6f-3f65f6c12059"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Billboards",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(8192)",
                oldMaxLength: 8192);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("c792e7cb-8dbf-4177-b2b1-ad60b68f2307"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });
        }
    }
}
