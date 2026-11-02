using LibraryBooksSystem.Models;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<BorrowRecord> records = new List<BorrowRecord>();
            List<Book> books = new List<Book> { 
            new Book 
            {
                Id = 1,
                Title = "Clean Code",
                Author = "Robert Martin",
                Category = "Programming",
                IsAvailable = true,
                Price = 130,
                StockQuantity = 40
            },
               new Book
               {
                   Id = 2,
                   Title = "The Hobbit",
                   Author = "J.R.R. Tolkien",
                   Category = "Novel",
                   IsAvailable = true,
                   Price = 188,
                   StockQuantity = 15,
               }};

            List<Member> members = new List<Member>
            {
                new Member(101,"shahd","Shahd1572006@gmail.com"),
                new Member(102,"Rahaf","Rahaf@gmail.com"),

            };
            List<Book> avialableBooks = new List<Book>();
            avialableBooks.AddRange(books.Where(b=>b.IsAvailable));

            BorrowServicecs service = new BorrowServicecs(avialableBooks, members,records);


            service.BorrowBook(101, 1);
            service.BorrowBook(102, 2);
            service.ReturnBook(101, 1);
            ReportService report = new ReportService(books, members, records);
            report.DisplayReport();

           

            
        }
    }
}
