using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addcorporatefunctionaltitletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageGUID",
                table: "HREmployee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
                name: "IdHRCorporateTitle",
                table: "HREmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IdHRFunctionalTitle",
                table: "HREmployee",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "HRCorporateTitle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinBasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxBasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxVehicleAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRCorporateTitle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRFunctionalTitle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionOrder = table.Column<int>(type: "int", nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRFunctionalTitle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRCorporateTitleId",
                table: "HREmployee",
                column: "HRCorporateTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_HREmployee_HRFunctionalTitleId",
                table: "HREmployee",
                column: "HRFunctionalTitleId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRCorporateTitle_HRCorporateTitleId",
                table: "HREmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_HREmployee_HRFunctionalTitle_HRFunctionalTitleId",
                table: "HREmployee");

            migrationBuilder.DropTable(
                name: "HRCorporateTitle");

            migrationBuilder.DropTable(
                name: "HRFunctionalTitle");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_HRCorporateTitleId",
                table: "HREmployee");

            migrationBuilder.DropIndex(
                name: "IX_HREmployee_HRFunctionalTitleId",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "HRCorporateTitleId",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "HRFunctionalTitleId",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "IdHRCorporateTitle",
                table: "HREmployee");

            migrationBuilder.DropColumn(
                name: "IdHRFunctionalTitle",
                table: "HREmployee");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageGUID",
                table: "HREmployee",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
