namespace Shop;

public class Order
{
    public  int Id { get; }

    private List<OrderItem> listItems;

    public IReadOnlyCollection<OrderItem> Items
    {
        get { return listItems; }
    }

    public int TotalCount
    {
        get { return listItems.Sum(item => item.Count); }
    }

    public decimal TotalPrice
    {
        get { return listItems.Sum(item => item.Price * item.Count); }
    }

    public Order(int id, IEnumerable<OrderItem> items)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items), " не может быть пустой");
        
        Id = id;
        this.listItems = new List<OrderItem>(items);
    }

    public OrderItem GetItem (int bookId)
    {
        var index =  listItems.FindIndex(x => x.BookId == bookId);
        if (index == -1)
            throw new InvalidOperationException("книги с таким Ид не существует");

        return listItems[index];
    }

    public void AddOrUpdateItem(Book book, int count)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book),"Попытка передать пустой ссылки");
        
        var index = listItems.FindIndex(x => x.BookId == book.Id);

        if (index == -1) // если книги нет
        {
            listItems.Add(new OrderItem(book.Id,count,book.Price)); // добавляем
        }
        else
        {
            listItems[index].Count += count; // меняем количество
        }
    }

    public void RemoveItem (int id)
    {
        var index = listItems.FindIndex(item => item.BookId == id);

        if (index == -1)
            throw new InvalidOperationException("Корзина не содериджит ");

        var item = listItems.SingleOrDefault(x => x.BookId == id);

        if (item == null)
            throw new InvalidOperationException("Корзина не содержит таких элементов");

        listItems.Remove(item);
    }

    
}