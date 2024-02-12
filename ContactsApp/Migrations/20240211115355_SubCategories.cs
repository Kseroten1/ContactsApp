using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactsApp.Migrations
{
    /// <inheritdoc />
    public partial class SubCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Contacts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Contacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.SubCategoryId);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Name",
                value: "Business");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Name",
                value: "Private");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Name",
                value: "Other");

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "SubCategoryId" },
                values: new object[] { new DateOnly(2000, 12, 25), 2 });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 2,
                columns: new[] { "DateOfBirth", "SubCategoryId" },
                values: new object[] { new DateOnly(1980, 1, 23), 2 });

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 3,
                columns: new[] { "DateOfBirth", "SubCategoryId" },
                values: new object[] { new DateOnly(1983, 2, 3), null });

            migrationBuilder.InsertData(
                table: "SubCategory",
                columns: new[] { "SubCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Boss" },
                    { 2, "Co-worker" },
                    { 3, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SubCategoryId",
                table: "Contacts",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_SubCategory_SubCategoryId",
                table: "Contacts",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_SubCategory_SubCategoryId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_SubCategoryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Contacts");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Name",
                value: "Family");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Name",
                value: "Friend");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Name",
                value: "Work");
        }
    }
}
