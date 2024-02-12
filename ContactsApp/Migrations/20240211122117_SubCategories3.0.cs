using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsApp.Migrations
{
    /// <inheritdoc />
    public partial class SubCategories30 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowCustomSubCategory",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "AllowCustomSubCategory",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "AllowCustomSubCategory",
                value: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "AllowCustomSubCategory",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowCustomSubCategory",
                table: "Categories");
        }
    }
}
