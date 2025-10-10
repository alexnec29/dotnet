namespace LibraryTracker.Records;

public record Borrower(int Id, string Name, List<Book> BorrowedBooks);
