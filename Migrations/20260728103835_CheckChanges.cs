using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class CheckChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HRPermissionEmployeeRoleDto");

            migrationBuilder.RenameColumn(
                name: "IDHRPermission",
                table: "HREmployeePermissionLinkMirror",
                newName: "IdHRPermission");

            migrationBuilder.RenameColumn(
                name: "IDHREmployee",
                table: "HREmployeePermissionLinkMirror",
                newName: "IdHREmployee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdHRPermission",
                table: "HREmployeePermissionLinkMirror",
                newName: "IDHRPermission");

            migrationBuilder.RenameColumn(
                name: "IdHREmployee",
                table: "HREmployeePermissionLinkMirror",
                newName: "IDHREmployee");

            migrationBuilder.CreateTable(
                name: "HRPermissionEmployeeRoleDto",
                columns: table => new
                {
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildOrder = table.Column<long>(type: "bigint", nullable: true),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateOnly = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteOnly = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditOnly = table.Column<bool>(type: "bit", nullable: false),
                    FontIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHRCompany = table.Column<long>(type: "bigint", nullable: false),
                    IdHREmployee = table.Column<long>(type: "bigint", nullable: true),
                    IdHRPermission = table.Column<long>(type: "bigint", nullable: true),
                    IdHRRole = table.Column<long>(type: "bigint", nullable: true),
                    IdParentPermission = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ParentOrder = table.Column<long>(type: "bigint", nullable: true),
                    PermissionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
