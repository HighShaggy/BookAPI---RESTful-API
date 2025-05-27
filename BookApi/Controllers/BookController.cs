using AutoMapper;
using BookApi.Data.Models;
using BookApi.Data.Interfaces;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class BookController(IBookRepository _bookRepository, BookService _bookService, IMapper _mapper) : ControllerBase
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
    public async Task<BookDto> GetById(int id)
    {
        var book= await _bookService.GetByIdAsync(id);
        return _mapper.Map<BookDto>(book);
    }

    [HttpGet]
    [Route("book")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll([FromQuery] int page = 1,
        [FromQuery] string? search = null)
    {
        var books = await _bookService.GetAll(page, search);
        var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(bookDtos);
    }
}