using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class chnagedatatypeaddIdHRCompanyColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HRRolePermissionLink",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HRRole",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HRPermission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "PositionOrder",
                table: "HRFunctionalTitle",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HRFunctionalTitle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HREmployeePermissionLink",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "PasswordExpiryPeriod",
                table: "HREmployee",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "LoginAttemptCount",
                table: "HREmployee",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HREmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HRCorporateTitle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdHRCompany",
                table: "HRBranch",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HRRole");

            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HRPermission");

            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HRFunctionalTitle");

            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HREmployeePermissionLink");

            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HRCorporateTitle");

            migrationBuilder.DropColumn(
                name: "IdHRCompany",
                table: "HRBranch");

            migrationBuilder.AlterColumn<int>(
                name: "PositionOrder",
                table: "HRFunctionalTitle",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "PasswordExpiryPeriod",
                table: "HREmployee",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoginAttemptCount",
                table: "HREmployee",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
