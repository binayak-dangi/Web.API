using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addmirrortable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HREmployeePermissionLinkMirror",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDHREmployee = table.Column<long>(type: "bigint", nullable: false),
                    IdHRCompany = table.Column<long>(type: "bigint", nullable: false),
                    IDHRPermission = table.Column<long>(type: "bigint", nullable: false),
                    CreateOnly = table.Column<bool>(type: "bit", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    EditOnly = table.Column<bool>(type: "bit", nullable: false),
                    DeleteOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HREmployeePermissionLinkMirror", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRPermissionEmployeeRoleDto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    IdHRCompany = table.Column<long>(type: "bigint", nullable: false),
                    IdHREmployee = table.Column<long>(type: "bigint", nullable: true),
                    IdHRRole = table.Column<long>(type: "bigint", nullable: true),
                    IdHRPermission = table.Column<long>(type: "bigint", nullable: true),
                    CreateOnly = table.Column<bool>(type: "bit", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    EditOnly = table.Column<bool>(type: "bit", nullable: false),
                    DeleteOnly = table.Column<bool>(type: "bit", nullable: false),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdParentPermission = table.Column<long>(type: "bigint", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentOrder = table.Column<long>(type: "bigint", nullable: true),
                    ChildOrder = table.Column<long>(type: "bigint", nullable: true),
                    FontIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HRRolePermissionLinkMirror",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHRRole = table.Column<long>(type: "bigint", nullable: false),
                    IdHRPermission = table.Column<long>(type: "bigint", nullable: false),
                    CreateOnly = table.Column<bool>(type: "bit", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    EditOnly = table.Column<bool>(type: "bit", nullable: false),
                    DeleteOnly = table.Column<bool>(type: "bit", nullable: false),
                    IdHRCompany = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRRolePermissionLinkMirror", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HREmployeePermissionLinkMirror");

            migrationBuilder.DropTable(
                name: "HRPermissionEmployeeRoleDto");

            migrationBuilder.DropTable(
                name: "HRRolePermissionLinkMirror");
        }
    }
}
