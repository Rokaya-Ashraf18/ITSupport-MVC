using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Services
{
    internal class BookServices
    {
        List<Book> books = new List<Book>();
        public void Add(Book book)
        {
            books.Add(book);
        }
        public void Delete(int id)
        {
            Book book = books.FirstOrDefault(x => x.Id == id);
           if(book != null) books.Remove(book);
        }
        public void Update(int id,Book _book)
        {
            Book book = books.FirstOrDefault(x => x.Id == id);
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
            var _books = books.OrderBy(x => x.Id).ToList();
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
            Book book = books.FirstOrDefault(x => x.Id==id);
          
                Console.WriteLine($"ID : {book.Id}\nTitle : {book.Title}\nAuthor : {book.Author}\nPrice : {book.Price}");
                Console.WriteLine($"Category : {book.Category}\nStockQuantity : {book.StockQuantity}\nIsAvailable : {book.IsAvailable}");
            
        }
        public void SearchById(int id)
        {
            Book book = books.FirstOrDefault(x => x.Id == id);
            if (book != null)
                DisplayBookByID(book.Id);
            else
                Console.WriteLine("There is no book with this id");
        }
        public void SearchByTitle(string title)
        {

            Book book = books.FirstOrDefault(x => x.Title.Equals( title, StringComparison.OrdinalIgnoreCase));
            if (book != null)
                DisplayBookByID(book.Id);
            else
                Console.WriteLine("There is no book with this title");

        }
        public bool IsBookAvailable(int id)
        {
            Book book = books.FirstOrDefault(x => x.Id == id);

            return book != null && book.IsAvailable;
        }

    }
}
