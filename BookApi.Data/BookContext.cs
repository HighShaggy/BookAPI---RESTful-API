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
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой" },
                new Book { Id = 2, Title = "Преступление и наказание", Author = "Фёдор Достоевский" },
                new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков" },
                new Book { Id = 4, Title = "Тихий Дон", Author = "Михаил Шолохов" },
                new Book { Id = 5, Title = "КОД: Тайный язык информатики", Author = "Чарльз Петцольд" },
                new Book { Id = 6, Title = "код да Винчи", Author = "Дэн Браун" },
                new Book { Id = 7, Title = "Чистый код", Author = "Роберт Мартин" }
            );
        }
    }
}