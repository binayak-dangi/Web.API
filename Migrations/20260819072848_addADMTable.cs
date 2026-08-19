using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addADMTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adm_MainHeading",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainHeading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainHeadingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystemDefined = table.Column<bool>(type: "bit", nullable: false),
                    OrderNo = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adm_MainHeading", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adm_Heading",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Heading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMainHeading = table.Column<long>(type: "bigint", nullable: false),
                    HeadingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSystemDefined = table.Column<bool>(type: "bit", nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNo = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adm_Heading", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adm_Heading_Adm_MainHeading_IdMainHeading",
                        column: x => x.IdMainHeading,
                        principalTable: "Adm_MainHeading",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adm_Element",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHeading = table.Column<long>(type: "bigint", nullable: false),
                    ElementHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogueCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystemDefined = table.Column<bool>(type: "bit", nullable: false),
                    IdParentElement = table.Column<long>(type: "bigint", nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNo = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updated_By = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adm_Element", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adm_Element_Adm_Heading_IdHeading",
                        column: x => x.IdHeading,
                        principalTable: "Adm_Heading",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adm_Element_IdHeading",
                table: "Adm_Element",
                column: "IdHeading");

            migrationBuilder.CreateIndex(
                name: "IX_Adm_Heading_IdMainHeading",
                table: "Adm_Heading",
                column: "IdMainHeading");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adm_Element");

            migrationBuilder.DropTable(
                name: "Adm_Heading");

            migrationBuilder.DropTable(
                name: "Adm_MainHeading");
        }
    }
}
