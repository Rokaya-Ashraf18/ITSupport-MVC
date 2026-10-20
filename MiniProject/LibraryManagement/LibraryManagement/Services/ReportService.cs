using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class ReportService
    {
        public int TotalBooks(List<Book> books)
        {
            return books.Count;
        }

        public int AvailableBooksCount(List<Book> books)
        {
            return books.Count(b => b.IsAvailable);
        }

        public int BorrowedBooksCount(List<Book> books)
        {
            return books.Count(b => !b.IsAvailable);
        }

        public int TotalMembers(List<Member> members)
        {
            return members.Count;
        }

        public int ActiveBorrowingsCount(List<BorrowRecord> records)
        {
            return records.Count(r => r.ReturnDate == null);
        }

        public string MostBorrowedBook(List<Book> books, List<BorrowRecord> records)
        {
            var result = records
                .GroupBy(r => r.BookId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (result == null)
            {
                return "No borrowing history available";
            }

            Book book = books.FirstOrDefault(b => b.Id == result.Key);

            if (book == null)
            {
                return "Book is not found";
            }

            return $"{book.Title} : {result.Count()} times";
        }

        public string MostActiveMember(List<Member> members, List<BorrowRecord> records)
        {
            var result = records
                .GroupBy(r => r.MemberID)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (result == null)
            {
                return "No borrowing history available";
            }

            Member member = members.FirstOrDefault(m => m.Id == result.Key);

            if (member == null)
            {
                return "Member is not found";
            }

            return $"{member.Name} : {result.Count()} borrowings";
        }

        public void DisplayReport(List<Book> books, List<Member> members, List<BorrowRecord> records)
        {
            Console.WriteLine();
            Console.WriteLine("LIBRARY REPORT");
            Console.WriteLine($"Total Books: {TotalBooks(books)}");
            Console.WriteLine($"Available Books: {AvailableBooksCount(books)}");
            Console.WriteLine($"Borrowed Books: {BorrowedBooksCount(books)}");
            Console.WriteLine($"Total Members: {TotalMembers(members)}");
            Console.WriteLine($"Active Borrowings: {ActiveBorrowingsCount(records)}");
            Console.WriteLine($"Most Borrowed Book: {MostBorrowedBook(books, records)}");
            Console.WriteLine($"Most Active Member: {MostActiveMember(members, records)}");
        }
    }
}