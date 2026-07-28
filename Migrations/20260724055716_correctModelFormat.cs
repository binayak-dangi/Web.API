using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class correctModelFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployeePermissionLink_HRRolePermissionLink_HRRolePermissionLinkId",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HREmployeePermissionLink_HRRolePermissionLinkId",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "Created_On",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "IdGUID",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "Updated_By",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "Updated_On",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "HRRolePermissionLinkId",
                table: "HREmployeePermissionLink");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "HRRolePermissionLink",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_On",
                table: "HRRolePermissionLink",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "IdGUID",
                table: "HRRolePermissionLink",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "HRRolePermissionLink",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HRRolePermissionLink",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Updated_By",
                table: "HRRolePermissionLink",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_On",
                table: "HRRolePermissionLink",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "HRRolePermissionLinkId",
                table: "HREmployeePermissionLink",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_HRRolePermissionLinkId",
                table: "HREmployeePermissionLink",
                column: "HRRolePermissionLinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployeePermissionLink_HRRolePermissionLink_HRRolePermissionLinkId",
                table: "HREmployeePermissionLink",
                column: "HRRolePermissionLinkId",
                principalTable: "HRRolePermissionLink",
                principalColumn: "Id");
        }
    }
}
