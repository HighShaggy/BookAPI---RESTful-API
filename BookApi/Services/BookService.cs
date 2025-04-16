using BookApi.Data.Interfaces;
using BookApi.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Services;

public class BookService(IBookRepository _bookRepository)
{
    public async Task<IActionResult> DeleteAsync(int id)
    {
        if (id <= 0)
        {
            return new BadRequestObjectResult("Invalid ID");
        }

        try
        {
            await _bookRepository.DeleteAsync(id);
            return new NoContentResult();
        }
        catch (KeyNotFoundException ex)
        {
            return new NotFoundObjectResult(ex.Message);
        }
        catch (Exception ex)
        {
            return new ObjectResult("Internal server error")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetById(id);
        if (book !=null)
        {
            return book;
        }
        else throw new Exception("Book not found");
    }

    public async Task<IEnumerable<Book>> GetAll(int page, string search)
    {
        var books = _bookRepository.GetAllAsync(page, search);

        return await books;
    }
}