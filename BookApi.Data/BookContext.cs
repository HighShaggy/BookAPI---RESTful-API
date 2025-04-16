using BookApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.SafeHandles;

namespace BookApi.Data
{
    public class BookContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public BookContext()
        {
        }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
                    "BookApi.Data", "books.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Лев Толстой" },
                new Author { Id = 2, Name = "Фёдор Достоевский" },
                new Author { Id = 3, Name = "Михаил Булгаков" },
                new Author { Id = 4, Name = "Михаил Шолохов" },
                new Author { Id = 5, Name = "Чарльз Петцольд" },
                new Author { Id = 6, Name = "Дэн Браун" },
                new Author { Id = 7, Name = "Роберт Мартин" }
            };
            modelBuilder.Entity<Author>().HasData(authors);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Война и мир", AuthorId = 1 },
                new Book { Id = 2, Title = "Преступление и наказание", AuthorId = 2 },
                new Book { Id = 3, Title = "Мастер и Маргарита", AuthorId = 3 },
                new Book { Id = 4, Title = "Тихий Дон", AuthorId = 4 },
                new Book { Id = 5, Title = "КОД: Тайный язык информатики", AuthorId = 5 },
                new Book { Id = 6, Title = "Код да Винчи", AuthorId = 6 },
                new Book { Id = 7, Title = "Чистый код", AuthorId = 7 },
                new Book { Id = 8, Title = "Собачье сердце", AuthorId = 3 }
            );
        }
    }
}