using BookApi.Models;

namespace BookApi.Services
{
    public interface IBookService
    {
        Book GetBookByISBN(string isbn);

        IEnumerable<Book> GetBooksByAuthor(string authorName);
    }
}
