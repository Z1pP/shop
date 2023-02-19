using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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
        (_, Cart cart) = GetOrderAndCart();

        if (cart == null)
            return View("Empty");

        var order = _orderRepository.GetById(cart.OrderId);
        OrderModel model = Map(order);

        return View(model);
    }


    [HttpPost, ActionName("AddBook")]
    public IActionResult AddBook(int id, int count)
    {
        (Order order, Cart cart) = GetOrderAndCart();

        var book = _bookRepository.GetBookById(id); //получение книги из БД по Ид
                                                    
        order.AddOrUpdateItem(book,count);

        SaveOrderAndCart(order, cart);
        
        return RedirectToAction("Index","Book", new {id});
    }


    [HttpPost, ActionName("RemoveBook")]
    public IActionResult RemoveBook(int bookId)
    {
        (Order order, Cart cart) = GetOrderAndCart();

        order.RemoveItem(bookId); // удаление элемента

        SaveOrderAndCart(order, cart);

        return RedirectToAction("Index", "Order");
    }
    private OrderModel Map(Order order)
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
    private (Order order, Cart cart) GetOrderAndCart ()
    {
        Order order;
        if (HttpContext.Session.TryGetCart(out Cart cart))
        {
            order = _orderRepository.GetById(cart.OrderId);
        }
        else
        {
            order = _orderRepository.CreateOrder();
            cart = new Cart(order.Id);
        }

        return (order, cart);
    }
    private void SaveOrderAndCart(Order order, Cart cart)
    {
        _orderRepository.UpdateOrder(order);
        cart.TotalCount = order.TotalCount;
        cart.TotalPrice = order.TotalPrice;

        HttpContext.Session.Set(cart); //запись данных в сессию
    }
}