using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Services
{
    internal class BookServices
    {
        List<Book> Books = new List<Book>();

        public BookServices(List<Book> books)
        {
            Books = books;
        }

        public void Add(Book book)
        {
            Books.Add(book);
        }
        public void Delete(int id)
        {
            Book book = Books.FirstOrDefault(x => x.Id == id);
           if(book != null) Books.Remove(book);
        }
        public void Update(int id,Book _book)
        {
            Book book = Books.FirstOrDefault(x => x.Id == id);
            if (book != null) 
            {
                book.Id = _book.Id;
                book.Title = _book.Title;
                book.Author = _book.Author;
                book.StockQuantity = _book.StockQuantity;
                book.Price = _book.Price;
                book.Category = _book.Category;
                book.IsAvailable = _book.IsAvailable;
            }
            
        }
        public void DisplayAllBooks() 
        {
            var _books = Books.OrderBy(x => x.Id).ToList();
            foreach (var item in _books)
            {
                //Console.WriteLine($"===========================");
                Console.WriteLine($"ID : {item.Id}\nTitle : {item.Title}\nAuthor : {item.Author}\nPrice : {item.Price}");
                Console.WriteLine($"Category : {item.Category}\nStockQuantity : {item.StockQuantity}\nIsAvailable : {item.IsAvailable}");
                Console.WriteLine($"===========================");
            }
        }
        public void DisplayBookByID(int id) 
        {
            Book  book = Books.FirstOrDefault(x => x.Id==id);


            Console.WriteLine($"ID : {book.Id}\nTitle : {book.Title}\nAuthor : {book.Author}\nPrice : {book.Price}");
            Console.WriteLine($"Category : {book.Category}\nStockQuantity : {book.StockQuantity}\nIsAvailable : {book.IsAvailable}");

        }
        public Book? SearchById(int id)
        {
            Book ?book = Books.FirstOrDefault(x => x.Id == id);
            return book;
            
        }
        public void SearchByTitle(string title)
        {

            Book book = Books.FirstOrDefault(x => x.Title.Equals( title, StringComparison.OrdinalIgnoreCase));
            if (book != null)
                DisplayBookByID(book.Id);
            else
                Console.WriteLine("There is no book with this title");

        }
        public bool IsBookAvailable(int id)
        {
            Book book = Books.FirstOrDefault(x => x.Id == id);

            return book != null && book.IsAvailable;
        }

    }
}
