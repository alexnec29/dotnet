using BookAPI.Persistence;

namespace BookAPI.Features.Books;

public class UpdateBookHandler
{
    private readonly BookContext _context;
    public UpdateBookHandler(BookContext context) => _context = context;

    public async Task<Book?> HandleAsync(int id, CreateBookRequest request)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
            return null;

        book.Title = request.Title;
        book.Author = request.Author;
        book.Year = request.Year;

        await _context.SaveChangesAsync();
        return book;
    }
}