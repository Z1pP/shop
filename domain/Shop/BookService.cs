
namespace Shop;

public class BookService
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> GetListBooks()
    {
        return _bookRepository.GetAllBook();
    }
    public List<Book> GetBooksByQuery(string query)
    {
        if(Book.IsIsbn(query))
            return _bookRepository.GetBooksByIsbn(query);
        
        return _bookRepository.GetAllBooksByTitleOrAuthor(query);
    }

    public Book GetDetail(int id)
    {
        return _bookRepository.GetBookById(id);
    }
}