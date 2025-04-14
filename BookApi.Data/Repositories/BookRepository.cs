using BookApi.Data.Interfaces;
using BookApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data.Repositories
{
    public class BookRepository(BookContext _booksContext) : IBookRepository
    {
        public async Task<int> AddAsync(Book book)
        {
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            }

            if (book.Id == 0)
            {
                var maxId = await _booksContext.Books.MaxAsync(b => (int?)b.Id) ?? 0;
                book.Id = maxId + 1;
            }

            await _booksContext.Books.AddAsync(book);
            await _booksContext.SaveChangesAsync();

            return book.Id;
        }
        public async Task<Book> GetById(int id)
        {
            var book = await _booksContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                throw new ArgumentException("book not found");
            }
            else
                return book;
        }
        public async Task DeleteAsync(int id)
        {
            var book = await _booksContext.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with id {id} not found");
            }

            _booksContext.Books.Remove(book);
            await _booksContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Book>> GetAllAsync(int page=1, string? search = null)
        {
            var query = _booksContext.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => 
                    EF.Functions.Like(b.Title.ToLower(), $"%{search.ToLower()}%"));
            } 
            int itemsPerPage = 5;
            query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            
            return await query.ToListAsync();
        }
    }


}
