using BookAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Features.Books;

public class GetAllBooksHandler
{
    private readonly BookContext _context;
    public GetAllBooksHandler(BookContext context) => _context = context;

    public async Task<IEnumerable<Book>> HandleAsync(int page = 1, int pageSize = 10)
    {
        return await _context.Books
            .OrderBy(b => b.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}