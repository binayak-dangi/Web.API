using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class renametabenameHRBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRBranche_HRBranchId",
                table: "HREmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HRBranche",
                table: "HRBranche");

            migrationBuilder.RenameTable(
                name: "HRBranche",
                newName: "HRBranch");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HRBranch",
                table: "HRBranch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRBranch_HRBranchId",
                table: "HREmployee",
                column: "HRBranchId",
                principalTable: "HRBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRBranch_HRBranchId",
                table: "HREmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HRBranch",
                table: "HRBranch");

            migrationBuilder.RenameTable(
                name: "HRBranch",
                newName: "HRBranche");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HRBranche",
                table: "HRBranche",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRBranche_HRBranchId",
                table: "HREmployee",
                column: "HRBranchId",
                principalTable: "HRBranche",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
