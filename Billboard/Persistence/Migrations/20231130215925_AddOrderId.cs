using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tariffs_TariffId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("64656322-9b5b-4245-8485-52fac5647c36"));

            migrationBuilder.RenameColumn(
                name: "TariffId",
                table: "Orders",
                newName: "SelectedTariffId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_TariffId",
                table: "Orders",
                newName: "IX_Orders_SelectedTariffId");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Picture",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("18c67a3e-883b-4a0b-8efb-47b36f5c09bf"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_OrderId",
                table: "Picture",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tariffs_SelectedTariffId",
                table: "Orders",
                column: "SelectedTariffId",
                principalTable: "Tariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Picture_Orders_OrderId",
                table: "Picture",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tariffs_SelectedTariffId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Picture_Orders_OrderId",
                table: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Picture_OrderId",
                table: "Picture");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("18c67a3e-883b-4a0b-8efb-47b36f5c09bf"));

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Picture");

            migrationBuilder.RenameColumn(
                name: "SelectedTariffId",
                table: "Orders",
                newName: "TariffId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SelectedTariffId",
                table: "Orders",
                newName: "IX_Orders_TariffId");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("64656322-9b5b-4245-8485-52fac5647c36"), "admin@Billboard.com", "Admin", "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tariffs_TariffId",
                table: "Orders",
                column: "TariffId",
                principalTable: "Tariffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
