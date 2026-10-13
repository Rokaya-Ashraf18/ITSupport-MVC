using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Models
{
    internal class BorrowRecord
    {
        public BorrowRecord(int borrowRecordId, int bookId, int memberID, DateTime borrowDate)
        {
            BorrowRecordId = borrowRecordId;
            BookId = bookId;
            MemberID = memberID;
            BorrowDate = borrowDate;
            ReturnDate = null;
        }

        public int BorrowRecordId { get;}
       public  int BookId { set; get; }
       public int MemberID{ set; get; }
       public DateTime BorrowDate { set; get; }
        public DateTime? ReturnDate { set; get; }

        

    }
}
