using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Author = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Title" },
                values: new object[,]
                {
                    { 1, "Лев Толстой", "Война и мир" },
                    { 2, "Фёдор Достоевский", "Преступление и наказание" },
                    { 3, "Михаил Булгаков", "Мастер и Маргарита" },
                    { 4, "Михаил Шолохов", "Тихий Дон" },
                    { 5, "Чарльз Петцольд", "КОД: Тайный язык информатики" },
                    { 6, "Дэн Браун", "код да Винчи" },
                    { 7, "Роберт Мартин", "Чистый код" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
