namespace Shop;

public class OrderItem
{
    public int BookId { get; }
    public int Count { get; }
    public decimal Price { get; }

    public OrderItem(int bookId, int count, decimal price)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(Count)," не может быть меньше либо равным 0"); 
                
        BookId = bookId;
        Count = count;
        Price = price;
    }
}