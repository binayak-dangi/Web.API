using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addNewlyAddedInHREmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isNewlyAdded",
                table: "HREmployee",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isNewlyAdded",
                table: "HREmployee");
        }
    }
}
