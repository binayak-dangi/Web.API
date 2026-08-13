using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeyInHREmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_IdHRCompany",
                table: "HREmployee",
                column: "IdHRCompany");

            migrationBuilder.AddForeignKey(
                name: "FK_HREmployee_HRCompany_IdHRCompany",
                table: "HREmployee",
                column: "IdHRCompany",
                principalTable: "HRCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRCompany_IdHRCompany",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_IdHRCompany",
                table: "HREmployee");
        }
    }
}
