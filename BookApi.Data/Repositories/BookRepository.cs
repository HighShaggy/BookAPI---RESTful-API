using BookApi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data.Repositories
{
    public class BookRepository(BookContext _booksContext) : IBookRepository 
    {
        // static List<Book> books = new List<Book>
        // {
        //     new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой" },
        //     new Book { Id = 2, Title = "Преступление и наказание", Author = "Фёдор Достоевский" },
        //     new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков" }
        //  };
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
        public Book GetById(int id)
        {
            var book = _booksContext.Books.FirstOrDefault(x => x.Id == id);
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
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _booksContext.Books.ToListAsync();
        }
    }


}
