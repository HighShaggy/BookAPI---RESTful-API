using BookApi;
using BookApi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class BookController : ControllerBase
{
    readonly IBookRepository _bookRepository;
    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

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
        try
        {
            await _bookRepository.DeleteAsync(id);
            return NoContent(); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
    [HttpGet]
    [Route("book/{id}")]
    public IActionResult GetById(int id)
    {
        var book = _bookRepository.GetById(id);
        if (book != null)
        {
            return Ok(book);
        }
        else return NotFound();
    }
    [HttpGet]
    [Route("book")]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        var books = _bookRepository.GetAllAsync();
        if (books == null)
        { return NotFound(); }
        else
            return Ok(books);
    }
}