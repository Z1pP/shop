namespace Shop;

public class OrderItem
{
    public int BookId { get; }

    private int count;
    public int Count
    {
        get { return count; }
        set 
        {
            ThrowIfInvalidCount(value); 
            count = value;
        }

    }
    public decimal Price { get; }

    public OrderItem(int bookId, int count, decimal price)
    {
        ThrowIfInvalidCount(count);    // проверка count 
        
        BookId = bookId;
        Count = count;
        Price = price;
    }

    public static void ThrowIfInvalidCount (int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(Count), " не может быть меньше либо равным 0");
    }
}