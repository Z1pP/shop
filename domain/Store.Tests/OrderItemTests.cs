using Shop;

namespace Store.Tests;

public class OrderItemTests
{
    [Fact]
    public void OrderItem_WithZeroCount_ThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            int count = 0;
            new OrderItem(1, count, 0m);
        });
    }
    [Fact]
    public void OrderItem_WithPositiveCount_SetsCount()
    {
        var orderItem = new OrderItem(1, 2, 3m);
        Assert.Equal(1,orderItem.BookId);
        Assert.Equal(2,orderItem.Count);
        Assert.Equal(3,orderItem.Price);
    }
}