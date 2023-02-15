using Shop;

namespace Store.Tests;

public class BookTests
{
    [Fact]
    public void IsIsbn_WithNull_ReturnFalse() // возвращает ложь если приходит null
    {
        bool actual = Book.IsIsbn(null);
        Assert.False(actual);
    }

    [Fact]
    public void IsIsbn_WithInvalidIsbn_ReturnFalse()
    {
        bool actual = Book.IsIsbn("ISBN 123");
        Assert.False(actual);
    }
    [Fact]
    public void IsIsbn_WithIsnb10_ReturnTrue()
    {
        bool actual = Book.IsIsbn("ISBN 1233-3453-23");
        Assert.True(actual);
    }
    [Fact]
    public void IsIsbn_WithIsnb13_ReturnTrue()
    {
        bool actual = Book.IsIsbn("ISBN 1233-3453-23234");
        Assert.True(actual);
    }
    [Fact]
    public void IsIsbn_WithTrashStart_ReturnFalse()
    {
        bool actual = Book.IsIsbn("xxx ISBN 1233-3453-23 yyy");
        Assert.False(actual);
    }
    
}