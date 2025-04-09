namespace BookApi.Data.Interfaces
{
    public interface IBookRepository
    {
        public Task<int> AddAsync(Book book);
        public Task DeleteAsync(int id);
        public Book GetById(int id);
        public Task<IEnumerable<Book>> GetAllAsync();

    }
}