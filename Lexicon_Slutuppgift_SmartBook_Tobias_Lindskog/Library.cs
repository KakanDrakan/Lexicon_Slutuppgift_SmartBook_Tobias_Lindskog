using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift_SmartBook_Tobias_Lindskog
{
    public class Library
    {
        public List<Book> books { get; set; } = new List<Book>();
        public List<User> users { get; set; } = new List<User>();
        
        public List<Book> GetListOfBooks()
        {
            return books;
        }

        public void AddBook()
        {  
            var book = new Book();
            Console.Write("Book title: ");
            book.title = Console.ReadLine();
            Console.Write("Author's name: ");
            book.author = Console.ReadLine();
            Console.Write("ISBN: ");
            var isbn = Console.ReadLine();
            if (!books.Any(b => b.ISBN == isbn)) book.ISBN = isbn;
            else throw new Exception("This ISBN is already in the library list. Are you trying to add an already existing book?");
            Console.Write("Category: ");
            book.category = Console.ReadLine();
            
            books.Add(book);
            books = books.OrderBy(b => b.author).ThenBy(b => b.title).ToList();
            LogEvent($"Added {book.ToString()} to library list");
            Console.WriteLine();
        }

        public Book AddBook(string title, string author, string ISBN, string category) //for testing
        {
            var book = new Book();
            book.title = title;
            book.author = author;
            book.ISBN = ISBN;
            book.category = category;
            books.Add(book);
            books = books.OrderBy(b => b.author).ThenBy(b => b.title).ToList();
            return book;
        }

        public void RemoveBook()
        {
            Console.Write("Book title or ISBN: ");
            var book = FindBook(Console.ReadLine());
            books.Remove(book);
            LogEvent($"Removed {book.ToStringWithoutISBN()} from library list");
        }

        public void BorrowBook()
        {
            Console.Write($"User ID: ");
            var user = FindUser(Console.ReadLine());
            Console.Write("Book title or ISBN: ");
            var book = FindBook(Console.ReadLine());
            book.Borrow(user);
            LogEvent($"User {user} borrowed {book.ToStringWithoutISBN()}.");
        }

        public void ReturnBook()
        {
            Console.Write("Book title or ISBN: ");
            var book = FindBook(Console.ReadLine());
            book.Return();
            LogEvent($"{book.ToStringWithoutISBN()} was returned.");
        }

        public void AddUser()
        {
            var user = new User();
            Console.Write("Username: ");
            user.name = Console.ReadLine();
            Console.Write("User ID: ");
            user.id = Console.ReadLine();
            Console.Write("Phone number: ");
            user.phone = Console.ReadLine();
            users.Add(user);
            users = users.OrderBy(b => b.name).ThenBy(b => b.id).ToList();
            LogEvent($"User {user.ToString()} was added!");
        }
        public void RemoveUser()
        {
            Console.Write("User ID: ");
            var user = FindUser(Console.ReadLine());
            var list = books.Where(b => b.borrower == user);
            if (list.Count()>0)
            {
                Console.Write($"User {user.name} has not returned all their borrowed books. {Environment.NewLine}" +
                    $"Proceed with user removal and return their borrowed books? (y/n): ");
                string input;
                do
                {
                    input = Console.ReadLine();
                } while (input == "y" || input == "n");
                if (input == "n") return;
                
                foreach (var book in list)
                {
                    book.borrower = null;
                }
            }
            users.Remove(user);
            Console.WriteLine($"User {user.name} has been removed");
            Console.WriteLine();
        }

        public Book FindBook(string nameOrISBN)
        {
            return books.FirstOrDefault(b => b.title == nameOrISBN || b.ISBN == nameOrISBN)
                ?? throw new Exception("A book with that title or ISBN was not found.");
        }

        public User FindUser(string id)
        {
            return users.FirstOrDefault(u => u.id == id) 
                ?? throw new Exception("A user with that ID was not found.");
        }

        public void SearchForBooks()
        {
            Console.Write("Book title, author or ISBN: ");
            var query = Console.ReadLine();
            var foundBooks = books.Where(b => b.title == query || b.author == query || b.ISBN == query)
                ?? throw new Exception("No books found");
            ListItems(foundBooks);
        }

        public void ListItems<T>(IEnumerable<T> list)
        {
            foreach (var item in list) 
                {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }

        public void ListBorrowedBooksByUser()
        {
            Console.Write("User ID: ");
            var input = Console.ReadLine();
            var user = FindUser(input);
            var foundBooks = books.Where(b => b.borrower == user)
                ?? throw new Exception($"User {user.name} hasn't borrowed any books");
            Console.WriteLine($"User {user.name} has borrowed the following book(s):");
            ListItems(foundBooks);
        }

        public void LogEvent(string msg)
        {
            File.AppendAllText("LibraryLog.txt", $"[{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {msg+Environment.NewLine}");
            Console.WriteLine(msg);
            Console.WriteLine();
        }

        public void ClearEventLog()
        {
            File.WriteAllText("LibraryLog.txt", "Library event log:");
            Console.WriteLine("The event log was cleared!");
            Console.WriteLine();
        }

        public void ExtractReportOfBorrowedBooks()
        {
            Console.WriteLine("Created file \"BorrowedBooksReport.txt\" containing:");
            Console.WriteLine("Borrowed books: ");
            File.WriteAllText("BorrowedBooksReport.txt", "Borrowed books:" + Environment.NewLine);
            foreach (var book in books.Where(b => b.borrower!=null))
            {
                File.AppendAllText("BorrowedBooksReport.txt", book.ToStringWithBorrower() + Environment.NewLine);
                Console.WriteLine(book.ToStringWithBorrower());
            }
            Console.WriteLine();
        }
    }
}
