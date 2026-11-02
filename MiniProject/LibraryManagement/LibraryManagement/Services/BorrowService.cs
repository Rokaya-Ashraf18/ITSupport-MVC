
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement.Services
{
    internal class BorrowServicecs
    {
        private readonly int borrow_limit = 3;

        public BorrowServicecs(List<Book> avialbleBooks, List<Member> members,List <BorrowRecord> records)
        {
            AvialbleBooks = avialbleBooks;
            Members = members;
            Records = records;
           
        }


        public List<Book> AvialbleBooks { get; set; }
        public List<Member> Members { get; set; }
        public List<BorrowRecord> Records { get; set; }

        public ServiceResult<BorrowRecord> BorrowBook(int memberId, int bookId)
        {
            MemberService memberService = new MemberService(Members);
            BookServices bookServices = new BookServices(AvialbleBooks);
            var message = "faild";


            if (memberService.GetMemberById(memberId) is null)
            {
                message = "Member not found.";
                return ServiceResult<BorrowRecord>.Fail(message);
            }

            var timesOfBorrow = 0;
            if (Records is not null) {
                timesOfBorrow = Records.Count(x => x.MemberID == memberId && x.ReturnDate is null);
            }
           

            bool userAllowedToBorrow() => timesOfBorrow < borrow_limit;

            if (!userAllowedToBorrow())
            {

                message = "user not Allowed To Borrow";
                return ServiceResult<BorrowRecord>.Fail(message);
            }
            var book = bookServices.FindById(bookId);
            if (book is null)
            {
                message = "Book is not Found";
                return ServiceResult<BorrowRecord>.Fail(message);

            }

            if (!book.IsAvailable)
            {
                message = "Book is not Available";
                return ServiceResult<BorrowRecord>.Fail(message);
            }



            message = "Book Borrowed Succeffully";
            string borrowId = Guid.NewGuid().ToString();
            var record = new BorrowRecord(borrowId, bookId, memberId, DateTime.Now);

            Records.Add(record);
            book.StockQuantity--;

            if (book.StockQuantity == 0)
            {
                book.IsAvailable = false;
            }
            return ServiceResult<BorrowRecord>.Ok(record, message);
        }

        public ServiceResult ReturnBook(int memberId, int bookId)
        {

            var message = "";
            var valid = Records.FirstOrDefault(x => x.MemberID == memberId && x.BookId == bookId && x.ReturnDate is null);
            if (valid is null)
            {
                message = "No active borrowing found for this member and book";
                return ServiceResult.Fail(message);
            }

            var book = AvialbleBooks.Find(x => x.Id == bookId);

            if (book is null)
            {
                return ServiceResult.Fail("Book not found");
            }

            valid.ReturnDate = DateTime.Now;

            book.StockQuantity++;

            book.IsAvailable = true;
            message = "Book Returned Successfully";
            return ServiceResult.Ok(message);

        }


    }
}