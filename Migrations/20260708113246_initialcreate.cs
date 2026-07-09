using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HRBranche",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BMEmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRBranche", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRPermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdParentPermission = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PermissionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentOrder = table.Column<long>(type: "bigint", nullable: false),
                    ChildOrder = table.Column<long>(type: "bigint", nullable: true),
                    FontIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRPermission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HREmployee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHRRole = table.Column<long>(type: "bigint", nullable: false),
                    IdHRBranch = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemporaryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastPasswordAlterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PasswordExpiryPeriod = table.Column<int>(type: "int", nullable: false),
                    PasswordHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginAttemptCount = table.Column<int>(type: "int", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HRRoleId = table.Column<long>(type: "bigint", nullable: false),
                    HRBranchId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HREmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HREmployee_HRBranche_HRBranchId",
                        column: x => x.HRBranchId,
                        principalTable: "HRBranche",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HREmployee_HRRole_HRRoleId",
                        column: x => x.HRRoleId,
                        principalTable: "HRRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HRRolePermissionLink",
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
                    HRPermissionId = table.Column<long>(type: "bigint", nullable: false),
                    HRRoleId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRRolePermissionLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HRRolePermissionLink_HRPermission_HRPermissionId",
                        column: x => x.HRPermissionId,
                        principalTable: "HRPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRRolePermissionLink_HRRole_HRRoleId",
                        column: x => x.HRRoleId,
                        principalTable: "HRRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HREmployeePermissionLink",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDHREmployee = table.Column<long>(type: "bigint", nullable: false),
                    IDHRPermission = table.Column<long>(type: "bigint", nullable: false),
                    CreateOnly = table.Column<bool>(type: "bit", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    EditOnly = table.Column<bool>(type: "bit", nullable: false),
                    DeleteOnly = table.Column<bool>(type: "bit", nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HREmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    HRPermissionId = table.Column<long>(type: "bigint", nullable: false),
                    HRRolePermissionLinkId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HREmployeePermissionLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HREmployeePermissionLink_HREmployee_HREmployeeId",
                        column: x => x.HREmployeeId,
                        principalTable: "HREmployee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HREmployeePermissionLink_HRPermission_HRPermissionId",
                        column: x => x.HRPermissionId,
                        principalTable: "HRPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HREmployeePermissionLink_HRRolePermissionLink_HRRolePermissionLinkId",
                        column: x => x.HRRolePermissionLinkId,
                        principalTable: "HRRolePermissionLink",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRBranchId",
                table: "HREmployee",
                column: "HRBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRRoleId",
                table: "HREmployee",
                column: "HRRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_HREmployeeId",
                table: "HREmployeePermissionLink",
                column: "HREmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_HRPermissionId",
                table: "HREmployeePermissionLink",
                column: "HRPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployeePermissionLink_HRRolePermissionLinkId",
                table: "HREmployeePermissionLink",
                column: "HRRolePermissionLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_HRRolePermissionLink_HRPermissionId",
                table: "HRRolePermissionLink",
                column: "HRPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_HRRolePermissionLink_HRRoleId",
                table: "HRRolePermissionLink",
                column: "HRRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HREmployeePermissionLink");

            migrationBuilder.DropTable(
                name: "HREmployee");

            migrationBuilder.DropTable(
                name: "HRRolePermissionLink");

            migrationBuilder.DropTable(
                name: "HRBranche");

            migrationBuilder.DropTable(
                name: "HRPermission");

            migrationBuilder.DropTable(
                name: "HRRole");
        }
    }
}
