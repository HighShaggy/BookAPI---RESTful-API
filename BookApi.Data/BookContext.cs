using Microsoft.EntityFrameworkCore;
namespace BookApi.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookContext() { }

        public BookContext(DbContextOptions<BookContext> options) : base(options) { } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "BookApi.Data", "books.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(new List<Book>()
            {
             new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой" },
             new Book { Id = 2, Title = "Преступление и наказание", Author = "Фёдор Достоевский" },
             new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков" }
            });
        }
    }
}
