using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addisDeletedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HRRolePermissionLink",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HRRole",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HRPermission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HRFunctionalTitle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HREmployee",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HRCorporateTitle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HRBranch",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HRRolePermissionLink");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HRRole");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HRPermission");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HRFunctionalTitle");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HRCorporateTitle");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HRBranch");
        }
    }
}
