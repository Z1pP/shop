using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.Controllers;

public class BookController : Controller
{
    private readonly BookService _bookService;
    
    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }
    // GET
    public IActionResult Index(int id)
    {
        var book = _bookService.GetDetail(id);
        return View(book);
    }
}