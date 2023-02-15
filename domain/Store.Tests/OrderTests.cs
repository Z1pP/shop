using System.Reflection.Metadata.Ecma335;
using Shop;

namespace Store.Tests;

public class OrderTests
{
    [Fact]
    public void Order_WithNullItems_ThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            List<OrderItem> listItems = null;
            new Order(1, listItems);
        });
    }

    [Fact]
    public void TotalCount_WithEmptyItems_ReturnZero()
    {
        var order = new Order(1, new OrderItem[0]);
        Assert.Equal(0,order.TotalCount);
    }
    [Fact]
    public void TotalPrice_WithEmptyItems_ReturnZero()
    {
        var order = new Order(1, new OrderItem[0]);
        Assert.Equal(0m,order.TotalPrice);
    }
    [Fact]
    public void TotalPrice_WithNonEmptyItems_CalculatesTotalCount()
    {
        var order = new Order(1,new[]
        {
            new OrderItem(1,3,10m),
            new OrderItem(2,5,100m)
        });
        
        Assert.Equal(3 * 10m + 5 * 100m,order.TotalPrice);
    }
    
}