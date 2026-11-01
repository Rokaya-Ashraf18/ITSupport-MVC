using LibraryManagement.Models;
namespace LibraryManagement.Services;
internal class LibraryBorrowCoordinator
{
    private readonly List<Book> books;
    private readonly List<Member> members;
    private readonly List<BorrowRecord> records;
    public LibraryBorrowCoordinator(List<Book> books, List<Member> members, List<BorrowRecord> records)
    {
        this.books = books;
        this.members = members;
        this.records = records;
    }
    public string Borrow(int memberId, int bookId)
    {
        if (!members.Any(m => m.Id == memberId))
            return "Member not found.";
        if (records.Count(r => r.MemberID == memberId && r.ReturnDate == null) >= 3)
            return "Borrow limit reached (3 books).";
        var b = books.FirstOrDefault(x => x.Id == bookId);
        if (b == null)
            return "Book not found.";
        if (!b.IsAvailable || b.StockQuantity <= 0)
            return "Book not available.";
        records.Add(new BorrowRecord(Guid.NewGuid().ToString(), bookId, memberId, DateTime.Now));
        b.StockQuantity--;
        b.IsAvailable = b.StockQuantity > 0;
        return "Book borrowed successfully.";
    }
    public string Return(int memberId, int bookId)
    {
        var r = records.FirstOrDefault(x => x.MemberID == memberId && x.BookId == bookId && x.ReturnDate == null);
        if (r == null)
            return "No active borrowing found.";
        var b = books.FirstOrDefault(x => x.Id == bookId);
        if (b == null)
            return "Book not found.";
        r.ReturnDate = DateTime.Now; b.StockQuantity++; b.IsAvailable = true;
        return "Book returned successfully.";
    }
}

