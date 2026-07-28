using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class correctForeignKeyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployeePermissionLink_HREmployee_HREmployeeId",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployeePermissionLink_HRPermission_HRPermissionId",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropForeignKey(
                name: "FK_HRRolePermissionLink_HRPermission_HRPermissionId",
                table: "HRRolePermissionLink");

            migrationBuilder.DropForeignKey(
                name: "FK_HRRolePermissionLink_HRRole_HRRoleId",
                table: "HRRolePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HRRolePermissionLink_HRPermissionId",
                table: "HRRolePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HRRolePermissionLink_HRRoleId",
                table: "HRRolePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HREmployeePermissionLink_HREmployeeId",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HREmployeePermissionLink_HRPermissionId",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropColumn(
                name: "HRPermissionId",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "HRRoleId",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "HREmployeeId",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropColumn(
                name: "HRPermissionId",
                table: "HREmployeePermissionLink");

            migrationBuilder.RenameColumn(
                name: "IDHRPermission",
                table: "HREmployeePermissionLink",
                newName: "IdHRPermission");

            migrationBuilder.RenameColumn(
                name: "IDHREmployee",
                table: "HREmployeePermissionLink",
                newName: "IdHREmployee");

            migrationBuilder.CreateIndex(
                name: "IX_HRRolePermissionLink_IdHRPermission",
                table: "HRRolePermissionLink",
                column: "IdHRPermission");

            migrationBuilder.CreateIndex(
                name: "IX_HRRolePermissionLink_IdHRRole",
                table: "HRRolePermissionLink",
                column: "IdHRRole");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_IdHREmployee",
                table: "HREmployeePermissionLink",
                column: "IdHREmployee");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_IdHRPermission",
                table: "HREmployeePermissionLink",
                column: "IdHRPermission");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployeePermissionLink_HREmployee_IdHREmployee",
                table: "HREmployeePermissionLink",
                column: "IdHREmployee",
                principalTable: "HREmployee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployeePermissionLink_HRPermission_IdHRPermission",
                table: "HREmployeePermissionLink",
                column: "IdHRPermission",
                principalTable: "HRPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRRolePermissionLink_HRPermission_IdHRPermission",
                table: "HRRolePermissionLink",
                column: "IdHRPermission",
                principalTable: "HRPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRRolePermissionLink_HRRole_IdHRRole",
                table: "HRRolePermissionLink",
                column: "IdHRRole",
                principalTable: "HRRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployeePermissionLink_HREmployee_IdHREmployee",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployeePermissionLink_HRPermission_IdHRPermission",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropForeignKey(
                name: "FK_HRRolePermissionLink_HRPermission_IdHRPermission",
                table: "HRRolePermissionLink");

            migrationBuilder.DropForeignKey(
                name: "FK_HRRolePermissionLink_HRRole_IdHRRole",
                table: "HRRolePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HRRolePermissionLink_IdHRPermission",
                table: "HRRolePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HRRolePermissionLink_IdHRRole",
                table: "HRRolePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HREmployeePermissionLink_IdHREmployee",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropIndex(
                name: "IX_HREmployeePermissionLink_IdHRPermission",
                table: "HREmployeePermissionLink");

            migrationBuilder.RenameColumn(
                name: "IdHRPermission",
                table: "HREmployeePermissionLink",
                newName: "IDHRPermission");

            migrationBuilder.RenameColumn(
                name: "IdHREmployee",
                table: "HREmployeePermissionLink",
                newName: "IDHREmployee");

            migrationBuilder.AddColumn<long>(
                name: "HRPermissionId",
                table: "HRRolePermissionLink",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HRRoleId",
                table: "HRRolePermissionLink",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HREmployeeId",
                table: "HREmployeePermissionLink",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HRPermissionId",
                table: "HREmployeePermissionLink",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_HRRolePermissionLink_HRPermissionId",
                table: "HRRolePermissionLink",
                column: "HRPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_HRRolePermissionLink_HRRoleId",
                table: "HRRolePermissionLink",
                column: "HRRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_HREmployeeId",
                table: "HREmployeePermissionLink",
                column: "HREmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_HRPermissionId",
                table: "HREmployeePermissionLink",
                column: "HRPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployeePermissionLink_HREmployee_HREmployeeId",
                table: "HREmployeePermissionLink",
                column: "HREmployeeId",
                principalTable: "HREmployee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployeePermissionLink_HRPermission_HRPermissionId",
                table: "HREmployeePermissionLink",
                column: "HRPermissionId",
                principalTable: "HRPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRRolePermissionLink_HRPermission_HRPermissionId",
                table: "HRRolePermissionLink",
                column: "HRPermissionId",
                principalTable: "HRPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HRRolePermissionLink_HRRole_HRRoleId",
                table: "HRRolePermissionLink",
                column: "HRRoleId",
                principalTable: "HRRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
