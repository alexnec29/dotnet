using BookAPI.Persistence;

namespace BookAPI.Features.Books;

public class DeleteBookHandler
{
    private readonly BookContext _context;
    public DeleteBookHandler(BookContext context) => _context = context;

    public async Task<bool> HandleAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
            return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}