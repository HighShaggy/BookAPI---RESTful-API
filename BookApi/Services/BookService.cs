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

    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetById(id);
        return book != null
            ? new OkObjectResult(book)
            : new NotFoundResult();
    }
    
    public ActionResult<IEnumerable<Book>> GetAll(int page, string search)
    {
        var books = _bookRepository.GetAllAsync(page, search);
        return books != null
            ? new OkObjectResult(books)
            : new NotFoundResult();
    }
}