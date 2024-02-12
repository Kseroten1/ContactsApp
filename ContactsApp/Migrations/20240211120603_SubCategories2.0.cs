using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactsApp.Migrations
{
    /// <inheritdoc />
    public partial class SubCategories20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_SubCategory_SubCategoryId",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory");

            migrationBuilder.RenameTable(
                name: "SubCategory",
                newName: "SubCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "SubCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories",
                column: "SubCategoryId");

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryId",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryId",
                keyValue: 2,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "SubCategoryId",
                keyValue: 3,
                column: "CategoryId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_SubCategories_SubCategoryId",
                table: "Contacts",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_SubCategories_SubCategoryId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "SubCategories");

            migrationBuilder.RenameTable(
                name: "SubCategories",
                newName: "SubCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategory",
                table: "SubCategory",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_SubCategory_SubCategoryId",
                table: "Contacts",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }
    }
}
