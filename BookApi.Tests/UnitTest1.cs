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
                    PublishedDate = new DateTime(1851, 10, 18)
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
    }

}