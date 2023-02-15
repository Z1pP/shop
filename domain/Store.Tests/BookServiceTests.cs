using Shop;
using Moq;

namespace Store.Tests;

public class BookServiceTests
{
    [Fact]
    public void GetBooksByQuery_WithIsbn_CallGetBooksByIsbn()
    {
        var bookRepositoryStub = new Mock<IBookRepository>();

        bookRepositoryStub.Setup(x => x.GetBooksByIsbn(It.IsAny<string>()))
            .Returns(new List<Book> { new Book(1, "", "", "","",0m) });
        bookRepositoryStub.Setup(x => x.GetAllBooksByTitleOrAuthor(It.IsAny<string>()))
            .Returns(new List<Book> { new Book(2, "", "", "","",0m) });
        var bookService = new BookService(bookRepositoryStub.Object);

        var actual = bookService.GetBooksByQuery("ISBN 12345-67890");
        Assert.Collection(actual,book => Assert.Equal(1, book.Id));
    }
    [Fact]
    public void GetBooksByQuery_WithAuthor_CallGetAllBooksByTitleOrAuthor()
    {
        var bookRepositoryStub = new Mock<IBookRepository>();

        bookRepositoryStub.Setup(x => x.GetBooksByIsbn(It.IsAny<string>()))
            .Returns(new List<Book> { new Book(1, "", "", "","",0m) });
        bookRepositoryStub.Setup(x => x.GetAllBooksByTitleOrAuthor(It.IsAny<string>()))
            .Returns(new List<Book> { new Book(2, "", "", "","",0m) });
        var bookService = new BookService(bookRepositoryStub.Object);

        var actual = bookService.GetBooksByQuery("Программирование");
        Assert.Collection(actual,book => Assert.Equal(2, book.Id));
    }
}