using LibraryManagement.Models;
using LibraryManagement.Services;
namespace LibraryManagement.UI;

internal static class LibraryConsole
{
    private static readonly List<Book> books = new();
    private static readonly List<Member> members = new();
    private static readonly List<BorrowRecord> records = new();
    private static readonly BookServices bookService = new(books);
    private static readonly MemberService memberService = new(members);
    private static readonly LibraryBorrowCoordinator borrowing = new(books, members, records);
    private static readonly ReportService reports = new(books, members, records);
    public static void Run()
    {
        while (true)
        {
            Console.WriteLine("\n=== LIBRARY MANAGEMENT SYSTEM ===\n1. Books\n2. Members\n3. Borrow\n4. Return\n5. Search\n6. Reports\n0. Exit");
            switch (ReadInt("Choice: "))
            {
                case 1:
                    BooksMenu();
                    break;
                case 2:
                    MembersMenu();
                    break;
                case 3:
                    Console.WriteLine(borrowing.Borrow(ReadInt("Member ID: "), ReadInt("Book ID: ")));
                    break;
                case 4:
                    Console.WriteLine(borrowing.Return(ReadInt("Member ID: "), ReadInt("Book ID: ")));
                    break;
                case 5:
                    Search();
                    break;
                case 6:
                    reports.DisplayReport();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int n) && n >= 0) return n;
            Console.WriteLine("Enter a valid non-negative number.");
        }
    }
    private static decimal ReadDecimal(string prompt)
    {
        while (true) 
        { 
            Console.Write(prompt); 
            if (decimal.TryParse(Console.ReadLine(), out decimal n) && n >= 0) 
                return n; 
            Console.WriteLine("Enter a valid non-negative price.");
        }
    }
    private static string ReadText(string prompt) 
    { 
        while (true) 
        { 
            Console.Write(prompt); 
            string? s = Console.ReadLine()?.Trim(); 
            if (!string.IsNullOrWhiteSpace(s))
                return s; 
            Console.WriteLine("Value cannot be empty."); 
        } 
    }
    private static void Print(Book b) => Console.WriteLine($"ID: {b.Id} | {b.Title} | {b.Author} | {b.Category} | Price: {b.Price} | Stock: {b.StockQuantity} | Available: {b.IsAvailable}");
    private static void BooksMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- BOOKS ---\n1. Add\n2. Update\n3. Delete\n4. List\n0. Back");
            switch (ReadInt("Choice: "))
            {
                case 1:
                    {
                        int id = ReadInt("ID: ");
                        if (books.Any(b => b.Id == id))
                        {
                            Console.WriteLine("Duplicate ID.");
                            break;
                        }
                        var b = InputBook(id);
                        bookService.Add(b);
                        Console.WriteLine("Book added.");
                        break;
                    }
                case 2:
                    {
                        int id = ReadInt("ID: ");
                        var b = books.FirstOrDefault(x => x.Id == id);
                        if (b == null)
                        {
                            Console.WriteLine("Not found.");
                            break;
                        }
                        if (records.Any(r => r.BookId == id && r.ReturnDate == null))
                        {
                            Console.WriteLine("Cannot update a book with active borrowings.");
                            break;
                        }
                        var updated = InputBook(id);
                        bookService.Update(id, updated);
                        Console.WriteLine("Updated.");
                        break;
                    }
                case 3:
                    {
                        int id = ReadInt("ID: ");
                        var b = books.FirstOrDefault(x => x.Id == id);
                        if (b == null)
                        {
                            Console.WriteLine("Not found.");
                            break;
                        }
                        if (records.Any(r => r.BookId == id))
                        {
                            Console.WriteLine("Cannot delete a book with borrowing history.");
                            break;
                        }
                        bookService.Delete(id);
                        Console.WriteLine("Deleted.");
                        break;
                    }
                case 4:
                    foreach (var b in books.OrderBy(b => b.Id))
                        Print(b);
                    if (books.Count == 0)
                        Console.WriteLine("No books.");
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
    private static Book InputBook(int id)
    {
        string title = ReadText("Title: "), author = ReadText("Author: "), category = ReadText("Category: ");
        decimal price = ReadDecimal("Price: ");
        int stock = ReadInt("Stock quantity: ");
        return new Book { Id = id, Title = title, Author = author, Category = category, Price = price, StockQuantity = stock, IsAvailable = stock > 0 };
    }
    private static void Copy(Book a, Book b)
    {
        a.Title = b.Title;
        a.Author = b.Author;
        a.Category = b.Category;
        a.Price = b.Price;
        a.StockQuantity = b.StockQuantity;
        a.IsAvailable = b.IsAvailable;
    }
    private static void MembersMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- MEMBERS ---\n1. Add\n2. Update\n3. Delete\n4. List\n0. Back");
            switch (ReadInt("Choice: "))
            {
                case 1:
                    {
                        var m = new Member { Id = ReadInt("ID: "), Name = ReadText("Name: "), Email = ReadText("Email: ") };
                        Console.WriteLine(memberService.Add(m) ? "Member added." : "Duplicate ID.");
                        break;
                    }
                case 2:
                    {
                        int id = ReadInt("ID: ");
                        Console.WriteLine(memberService.Update(id, ReadText("Name: "), ReadText("Email: ")) ? "Updated." : "Member not found.");
                        break;
                    }
                case 3:
                    {
                        int id = ReadInt("ID: ");
                        if (records.Any(r => r.MemberID == id))
                        {
                            Console.WriteLine("Cannot delete member with borrowing history.");
                            break;
                        }
                        Console.WriteLine(memberService.Delete(id, records) ? "Deleted." : "Member not found or has active borrowings.");
                        break;
                    }
                case 4:
                    foreach (var m in memberService.All()) Console.WriteLine($"ID: {m.Id} | {m.Name} | {m.Email}");
                    if (members.Count == 0) Console.WriteLine("No members.");
                    break;
                case 0:
                    return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }
    private static void Search()
    {
        Console.WriteLine("1. Title\n2. Author\n3. Available\n4. Member name");
        int option = ReadInt("Choice: ");
        if (option == 4)
        {
            string q = ReadText("Name: ");
            var result = members.Where(m => m.Name.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();
            foreach (var m in result) Console.WriteLine($"{m.Id} | {m.Name}");
            if (result.Count == 0) Console.WriteLine("No matches.");
            return;
        }
        IEnumerable<Book> found = option switch { 1 => books.Where(b => b.Title.Contains(ReadText("Title: "), StringComparison.OrdinalIgnoreCase)), 2 => books.Where(b => b.Author.Contains(ReadText("Author: "), StringComparison.OrdinalIgnoreCase)), 3 => books.Where(b => b.IsAvailable), _ => Enumerable.Empty<Book>() };
        var results = found.OrderBy(b => b.Title).ToList();
        foreach (var b in results) Print(b);
        if (results.Count == 0) Console.WriteLine("No matches.");
    }
}
