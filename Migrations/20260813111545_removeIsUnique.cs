using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class removeIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HRRole_RoleName",
                table: "HRRole");

            migrationBuilder.DropIndex(
                name: "IX_HRFunctionalTitle_PositionHead",
                table: "HRFunctionalTitle");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_Username",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HRCorporateTitle_LevelGrade",
                table: "HRCorporateTitle");

            migrationBuilder.DropIndex(
                name: "IX_HRCompany_CompanyName",
                table: "HRCompany");

            migrationBuilder.DropIndex(
                name: "IX_HRBranch_BranchName",
                table: "HRBranch");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "HRRole",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PositionHead",
                table: "HRFunctionalTitle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "HREmployee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LevelGrade",
                table: "HRCorporateTitle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "HRCompany",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchName",
                table: "HRBranch",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "HRRole",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PositionHead",
                table: "HRFunctionalTitle",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "HREmployee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LevelGrade",
                table: "HRCorporateTitle",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "HRCompany",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchName",
                table: "HRBranch",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_HRRole_RoleName",
                table: "HRRole",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HRFunctionalTitle_PositionHead",
                table: "HRFunctionalTitle",
                column: "PositionHead",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_Username",
                table: "HREmployee",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HRCorporateTitle_LevelGrade",
                table: "HRCorporateTitle",
                column: "LevelGrade",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HRCompany_CompanyName",
                table: "HRCompany",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HRBranch_BranchName",
                table: "HRBranch",
                column: "BranchName",
                unique: true);
        }
    }
}
