using System.Text.Json;
namespace Lexicon_Slutuppgift_SmartBook_Tobias_Lindskog
{
    public class Program
    {
        static void Main(string[] args)
        {
            var lib = new Library();
            bool isAlive = true;
            while (isAlive)
            {
                try
                {
                    (lib, isAlive) = LibraryMenu(lib);
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }
        public static (Library library, bool isAlive) LibraryMenu(Library lib)
        {
            bool isAlive = true;
            //{
                Console.WriteLine("You're in the main menu. Navigate by typing numbers:");
                Console.WriteLine($"0. Quit application {Environment.NewLine}" +
                    $"1. Add book {Environment.NewLine}" +
                    $"2. Remove book {Environment.NewLine}" +
                    $"3. List all books {Environment.NewLine}" +
                    $"4. Search for books {Environment.NewLine}" +
                    $"5. Add user {Environment.NewLine}" +
                    $"6. Remove user {Environment.NewLine}" +
                    $"7. List all users {Environment.NewLine}" +
                    $"8. Borrow book {Environment.NewLine}" +
                    $"9. Return book {Environment.NewLine}" +
                    $"10. List a user's borrowed books {Environment.NewLine}" +
                    $"11. Extract a report of borrowed books {Environment.NewLine}" +
                    $"12. Clear event log \"LibraryLog.txt\"{Environment.NewLine}" +
                    $"13. Save library to JSON {Environment.NewLine}" +
                    $"14. Load library from JSON{Environment.NewLine}" +
                    $"15. Load a sample library");
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "0":
                        isAlive = false; break;
                    case "1":
                        lib.AddBook(); break;
                    case "2":
                        lib.RemoveBook(); break;
                    case "3":
                        lib.ListItems(lib.books); break;
                    case "4":
                        lib.SearchForBooks(); break;
                    case "5":
                        lib.AddUser(); break;
                    case "6":
                        lib.RemoveUser(); break;
                    case "7":
                        lib.ListItems(lib.users); break;
                    case "8":
                        lib.BorrowBook(); break;
                    case "9":
                        lib.ReturnBook(); break;
                    case "10":
                        lib.ListBorrowedBooksByUser(); break;
                    case "11":
                        lib.ExtractReportOfBorrowedBooks(); break;
                    case "12":
                        lib.ClearEventLog(); break;
                    case "13":
                        SaveLibrary(lib); break;
                    case "14":
                        lib = LoadLibrary(); break;
                    case "15":
                    lib = LoadSampleLibrary(); break;
                    default:
                        Console.WriteLine("Invalid input, try again"); break;
                        
                }
            return (lib, isAlive);
                
            //} while (isAlive);
        }

        public static Library LoadLibrary()
        {
            Console.WriteLine("Library loaded from \"library.json\"");
            Console.WriteLine();
            return JsonSerializer.Deserialize<Library>(File.ReadAllText("library.json"));
        }

        public static void SaveLibrary(Library lib)
        {
            File.WriteAllText("library.json", JsonSerializer.Serialize(lib));
            Console.WriteLine("Library saved to \"library.json\"");
            Console.WriteLine();
        }

        public static Library LoadSampleLibrary()
        {
            Console.WriteLine("Library loaded from \"sampleLibrary.json\"");
            Console.WriteLine();
            return JsonSerializer.Deserialize<Library>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "sampleLibrary.json")));
        }
    }
}
