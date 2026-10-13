using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Services
{
    internal class BorrowServicecs
    {
        private readonly int borrow_limit=3;

        public BorrowServicecs(List<Book> avialbleBooks, List<Member> members, List<BorrowRecord> records)
        {
            AvialbleBooks = avialbleBooks;
            Members = members;
            Records = records;
        }


        public List<Book> AvialbleBooks { get; set; }
        public List<Member> Members { get; set; }
        public List<BorrowRecord> Records { get; set; }

        public 


    }
}
