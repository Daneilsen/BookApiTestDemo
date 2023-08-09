using BookApi.Models;

namespace BookApi.Services
{
    public interface IBookService
    {
        Book GetBookByISBN(string isbn);
    }
}
