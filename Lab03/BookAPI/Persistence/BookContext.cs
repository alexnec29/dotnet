using Microsoft.EntityFrameworkCore;
using BookAPI.Features.Books;

namespace BookAPI.Persistence;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
}