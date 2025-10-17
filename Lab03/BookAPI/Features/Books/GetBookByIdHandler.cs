using BookAPI.Persistence;

namespace BookAPI.Features.Books;

public class GetBookByIdHandler
{
    private readonly BookContext _context;
    public GetBookByIdHandler(BookContext context) => _context = context;

    public async Task<Book?> HandleAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }
}