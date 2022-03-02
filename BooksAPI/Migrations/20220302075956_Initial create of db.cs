using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksAPI.Migrations
{
    public partial class Initialcreateofdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "Price", "Title" },
                values: new object[] { 1, "A. A. Milne", 19.25, "Winnie-the-Pooh" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "Price", "Title" },
                values: new object[] { 2, "Jane Austen", 5.4900000000000002, "Pride and Prejudice" });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "Price", "Title" },
                values: new object[] { 3, "William Shakespeare", 6.9500000000000002, "Romeo and Juliet" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
