using Microsoft.AspNetCore.Mvc;


namespace Shop.Web.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly BookService _bookService;

        public SearchController(BookService bookService)
        {
            _bookService = bookService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var books = _bookService.GetListBooks();
            return View(books);
        }
        
        [HttpPost, ActionName("Search")]
        public IActionResult Search (string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return View("Index", null);
            
            var book = _bookService.GetBooksByQuery(query);
 
            return View("Index",book);
        }


    }
}