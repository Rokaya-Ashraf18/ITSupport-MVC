using System;
using System.Collections.Generic;
using System.Linq;

using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class ReportService
    {
        public ReportService(List<Book> books, List<Member> members, List<BorrowRecord> records)
        {
            Books = books;
            Members = members;
            Records = records;
           

        }
        public List<Book> AvialableBooks;
        public  List<Book> Books { set; get; }
        public List <Member> Members { set; get; }
        public List<BorrowRecord> Records { set; get; }
        public int TotalBooks()
        {
            return Books.Count;
        }

        public int AvailableBooksCount()
        {
            return Books.Count(b => b.IsAvailable);
        }

        public int BorrowedBooksCount()
        {
            return Records.Count();
        }
        public int TotalMembers()
        {
            return Members.Count;
        }

        public int ActiveBorrowingsCount()
        {
            return Records.Count(r => r.ReturnDate == null);
        }

        public string MostBorrowedBook()
        {
            var result = Records
                .GroupBy(r => r.BookId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (result == null)
            {
                return "No borrowing history available";
            }

            Book book = Books.FirstOrDefault(b => b.Id == result.Key);

            if (book == null)
            {
                return "Book is not found";
            }

            return $"{book.Title} : {result.Count()} times";
        }

        public string MostActiveMember()
        {
            var result = Records
                .GroupBy(r => r.MemberID)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (result == null)
            {
                return "No borrowing history available";
            }

            Member member = Members.FirstOrDefault(m => m.Id == result.Key);

            if (member == null)
            {
                return "Member is not found";
            }

            return $"{member.Name} : {result.Count()} borrowings";
        }

        public void DisplayReport()
        {
            Console.WriteLine();
            Console.WriteLine("LIBRARY REPORT");
            Console.WriteLine($"Total Books: {TotalBooks()}");
            Console.WriteLine($"Available Books: {AvailableBooksCount()}");
            Console.WriteLine($"Borrowed Books: {BorrowedBooksCount()}");
            Console.WriteLine($"Total Members: {TotalMembers()}");
            Console.WriteLine($"Active Borrowings: {ActiveBorrowingsCount()}");
            Console.WriteLine($"Most Borrowed Book: {MostBorrowedBook()}");
            Console.WriteLine($"Most Active Member: {MostActiveMember()}");
        }
    }
}