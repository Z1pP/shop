namespace Shop
{
    public interface IBookRepository
    {
        public List<Book> GetAllBook ();
        public List<Book> GetBooksByIsbn(string isbn);
        public List<Book> GetAllBooksByTitleOrAuthor (string title);
        public Book GetBookById(int id);
        List<Book> GetBooksById(IEnumerable<int> booksId);
    }
}