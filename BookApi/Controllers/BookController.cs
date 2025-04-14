using BookApi.Data.Models;
using BookApi.Data.Interfaces;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class BookController(IBookRepository _bookRepository, BookService _bookService) : ControllerBase
{
    [HttpPut]
    [Route("book")]
    public IActionResult Add(Book book)
    {
        return Ok(new { id = _bookRepository.AddAsync(book) });
    }

    [HttpDelete]
    [Route("book/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _bookService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("book/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return await _bookService.GetByIdAsync(id);
    }

    [HttpGet]
    [Route("book")]
    public ActionResult<IEnumerable<Book>> GetAll([FromQuery] int page = 1,
        [FromQuery] string? search = null)
    {
        return _bookService.GetAll(page, search);
    }
}