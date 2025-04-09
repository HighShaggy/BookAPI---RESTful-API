using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookApi.Data
{
    public class BookContextFactory : IDesignTimeDbContextFactory<BookContext>
    {
        public BookContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookContext>();
            optionsBuilder.UseSqlite("Data Source=books.db");
            return new BookContext(optionsBuilder.Options);
        }
    }
}