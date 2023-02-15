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

    public void AddItem(Book book, int count)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book),"Попытка передать пустой ссылки");
        
        var item = listItems.SingleOrDefault(x => x.BookId == book.Id);

        if (item == null) // если книги нет
            listItems.Add(new OrderItem(book.Id, count, book.Price)); //Добавляем
        else
        {
            listItems.Remove(item); //удаляем книгу
            listItems.Add(new OrderItem(book.Id, item.Count + count, book.Price));
        }
    }
}