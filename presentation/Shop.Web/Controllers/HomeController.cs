using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models;

namespace Shop.Web.Controllers;

public class HomeController : Controller
{
    private readonly BookService _bookService;

    public HomeController(BookService bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        return View(_bookService.GetListBooks());
    }
}
