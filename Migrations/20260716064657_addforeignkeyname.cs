using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addforeignkeyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRBranch_HRBranchId",
                table: "HREmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRCorporateTitle_HRCorporateTitleId",
                table: "HREmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRFunctionalTitle_HRFunctionalTitleId",
                table: "HREmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRRole_HRRoleId",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_HRBranchId",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_HRCorporateTitleId",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_HRFunctionalTitleId",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_HRRoleId",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "HRBranchId",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "HRCorporateTitleId",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "HRFunctionalTitleId",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "HRRoleId",
                table: "HREmployee");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_IdHRBranch",
                table: "HREmployee",
                column: "IdHRBranch");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_IdHRCorporateTitle",
                table: "HREmployee",
                column: "IdHRCorporateTitle");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_IdHRFunctionalTitle",
                table: "HREmployee",
                column: "IdHRFunctionalTitle");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_IdHRRole",
                table: "HREmployee",
                column: "IdHRRole");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRBranch_IdHRBranch",
                table: "HREmployee",
                column: "IdHRBranch",
                principalTable: "HRBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRCorporateTitle_IdHRCorporateTitle",
                table: "HREmployee",
                column: "IdHRCorporateTitle",
                principalTable: "HRCorporateTitle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRFunctionalTitle_IdHRFunctionalTitle",
                table: "HREmployee",
                column: "IdHRFunctionalTitle",
                principalTable: "HRFunctionalTitle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRRole_IdHRRole",
                table: "HREmployee",
                column: "IdHRRole",
                principalTable: "HRRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRBranch_IdHRBranch",
                table: "HREmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRCorporateTitle_IdHRCorporateTitle",
                table: "HREmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRFunctionalTitle_IdHRFunctionalTitle",
                table: "HREmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRRole_IdHRRole",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_IdHRBranch",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_IdHRCorporateTitle",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_IdHRFunctionalTitle",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_IdHRRole",
                table: "HREmployee");

            migrationBuilder.AddColumn<long>(
                name: "HRBranchId",
                table: "HREmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HRCorporateTitleId",
                table: "HREmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HRFunctionalTitleId",
                table: "HREmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HRRoleId",
                table: "HREmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRBranchId",
                table: "HREmployee",
                column: "HRBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRCorporateTitleId",
                table: "HREmployee",
                column: "HRCorporateTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRFunctionalTitleId",
                table: "HREmployee",
                column: "HRFunctionalTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRRoleId",
                table: "HREmployee",
                column: "HRRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRBranch_HRBranchId",
                table: "HREmployee",
                column: "HRBranchId",
                principalTable: "HRBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRCorporateTitle_HRCorporateTitleId",
                table: "HREmployee",
                column: "HRCorporateTitleId",
                principalTable: "HRCorporateTitle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRFunctionalTitle_HRFunctionalTitleId",
                table: "HREmployee",
                column: "HRFunctionalTitleId",
                principalTable: "HRFunctionalTitle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRRole_HRRoleId",
                table: "HREmployee",
                column: "HRRoleId",
                principalTable: "HRRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
