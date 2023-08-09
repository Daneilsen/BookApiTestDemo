using BookApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{isbn}")]
        public IActionResult GetBook(string isbn)
        {
            var book = _bookService.GetBookByISBN(isbn);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("author/{authorName}")]
        public IActionResult GetBooksByAuthor(string authorName)
        {
            var books = _bookService.GetBooksByAuthor(authorName);
            if (!books.Any())
            {
                return NotFound();
            }
            return Ok(books);
        }

    }
}
