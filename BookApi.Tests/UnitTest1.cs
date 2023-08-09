using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using BookApi.Controllers;
using BookApi.Models;
using BookApi.Services;

namespace BookApi.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void GetBook_ReturnsBook_WhenBookExists()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(svc => svc.GetBookByISBN("9780765382030")).Returns(
                new Book
                {
                    Id = 1,
                    Title = "The Three-Body Problem",
                    Author = "Cixin Liu",
                    ISBN = "9780765382030",
                    PublishedDate = new DateTime(2016, 1, 12)
                });

            var controller = new BookController(mockBookService.Object);

            // Act
            var result = controller.GetBook("9780765382030");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBook = Assert.IsType<Book>(okResult.Value);
            Assert.Equal("The Three-Body Problem", returnedBook.Title);
            Assert.Equal("Cixin Liu", returnedBook.Author);
        }

        [Fact]
        public void GetBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(svc => svc.GetBookByISBN("0987654321")).Returns((Book)null); // Simulate that the book doesn't exist

            var controller = new BookController(mockBookService.Object);

            // Act
            var result = controller.GetBook("0987654321");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetBooksByAuthor_ReturnsBooks_WhenAuthorExists()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            var mockBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "John Doe" },
                new Book { Id = 2, Title = "Book 2", Author = "John Doe" }
            };

            mockBookService.Setup(svc => svc.GetBooksByAuthor("John Doe")).Returns(mockBooks);

            var controller = new BookController(mockBookService.Object);

            // Act
            var result = controller.GetBooksByAuthor("John Doe");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBooks = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(2, returnedBooks.Count);
            Assert.Equal("John Doe", returnedBooks.First().Author);
        }

        [Fact]
        public void GetBooksByAuthor_ReturnsNotFound_WhenAuthorDoesNotExist()
        {
            // Arrange
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(svc => svc.GetBooksByAuthor("Jane Doe")).Returns(new List<Book>());

            var controller = new BookController(mockBookService.Object);

            // Act
            var result = controller.GetBooksByAuthor("Jane Doe");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }

}