using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models;

namespace Shop.Web.Controllers;

public class OrderController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IOrderRepository _orderRepository;
    
    public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository)
    {
        _bookRepository = bookRepository;
        _orderRepository = orderRepository;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.TryGetCard(out Cart cart)) //получение данных из сессии
        {
            var order = _orderRepository.GetById(cart.OrderId);
            OrderModel model = Map(order);

            return View(model);
        }

        return View("Empty");
    }

    public OrderModel Map(Order order)
    {
        var booksId = order.Items.Select(item => item.BookId);
        var books = _bookRepository.GetBooksById(booksId);

        var itemMode = from item in order.Items
                        join book in books on item.BookId 
                        equals book.Id 
                        select new OrderItemModel
                        {
                            BookId = book.Id,
                            Title = book.Title,
                            Author = book.Author,
                            Price = item.Price,
                            Count = item.Count
                        };
        
        return new OrderModel
        {
            Id = order.Id,
            Items = itemMode.ToList(),
            TotalCount = order.TotalCount,
            TotalPrice = order.TotalPrice
        };
    }

    [HttpPost, ActionName("Add")]
    public IActionResult Add(int id, int count)
    {
        Order order;
        Cart cart;
        
        if (HttpContext.Session.TryGetCard(out cart)) //получение данных из сессии
        {
            order = _orderRepository.GetById(cart.OrderId);
        }
        else
        {
            order = _orderRepository.Create();
            cart = new Cart(order.Id);
        }

        var book = _bookRepository.GetBookById(id);        
        order.AddItem(book,count);

        _orderRepository.Update(order);

        cart.TotalCount = order.TotalCount;
        cart.TotalPrice = order.TotalPrice;

        HttpContext.Session.Set(cart); //запись данных в сессию
        
        return RedirectToAction("Index","Book", new {id});
    }
}