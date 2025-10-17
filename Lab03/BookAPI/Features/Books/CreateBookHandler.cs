using BookAPI.Persistence;

namespace BookAPI.Features.Books;

public class CreateBookHandler
{
    private readonly BookContext _context;
    public CreateBookHandler(BookContext context) => _context = context;

    public async Task<Book> HandleAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Year = request.Year
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }
}