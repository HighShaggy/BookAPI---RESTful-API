using BookApi.Data.Models;

namespace BookApi.Data.Interfaces
{
    public interface IBookRepository
    {
        public Task<int> AddAsync(Book book);
        public Task DeleteAsync(int id);
        public Task<Book>GetById(int id);
        public Task<IEnumerable<Book>> GetAllAsync(int page=1, string? search = null);
    }
}