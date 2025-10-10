using LibraryTracker.Records;

//List of books
List<Book> books = new();

//init-only librarian variable
var librarian = new Librarian
{
    Name = "Alex",
    Email = "alex@gmail.com",
    LibrarySection = "Fiction"
};

//Printing librarian
Console.WriteLine($"Librarian: {librarian.Name}, Section: {librarian.LibrarySection}");
Console.WriteLine();

//Entering a list of books
Console.WriteLine("Enter book details (leave title empty to finish):");

while (true)
{
    Console.Write("Book Title: ");
    string? title = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(title))
        break;

    Console.Write("Author: ");
    string? author = Console.ReadLine();

    Console.Write("Year Published: ");
    if (!int.TryParse(Console.ReadLine(), out int year))
        year = DateTime.Now.Year;

    books.Add(new Book(title, author ?? "Unknown", year));
}

Console.WriteLine("All Entered Books:\n");
foreach (var book in books)
{
    Console.WriteLine($"{book.Title} by {book.Author} ({book.YearPublished})");
}

//Cloning borrower
var borrower1 = new Borrower(1, "John Doe", new List<Book>());
var borrower2 = borrower1 with { BorrowedBooks = borrower1.BorrowedBooks.Append(books.FirstOrDefault()!).ToList() };

Console.WriteLine($"Borrower cloned with new book: {borrower2.Name} borrowed {borrower2.BorrowedBooks.Count} book(s)\n");

//Pattern matching with local method
Console.WriteLine("Pattern Matching:\n");
DisplayInfo(books.FirstOrDefault()!);
DisplayInfo(borrower2);
DisplayInfo("Random text");

//lambda function for recent books
Console.WriteLine("Books Published After 2010:\n");
var recentBooks = books.Where(static b => b.YearPublished > 2010);
foreach (var book in recentBooks)
{
    Console.WriteLine($"{book.Title} ({book.YearPublished})");
}
//local function for pattern matching
static void DisplayInfo(object obj)
{
    switch (obj)
    {
        case Book b:
            Console.WriteLine($"Book: {b.Title} ({b.YearPublished})");
            break;
        case Borrower br:
            Console.WriteLine($"Borrower: {br.Name}, Borrowed Books: {br.BorrowedBooks.Count}");
            break;
        default:
            Console.WriteLine("Unknown type");
            break;
    }
}
