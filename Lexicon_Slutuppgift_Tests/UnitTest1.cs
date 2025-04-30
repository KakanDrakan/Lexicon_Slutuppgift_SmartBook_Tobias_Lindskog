using Lexicon_Slutuppgift_SmartBook_Tobias_Lindskog;

namespace Lexicon_Slutuppgift_Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddBooksAndCheckSort()
        {
            (List<Book> manuallySorted, List<Book> automaticallySorted) = AddSomeBooksToList(new Library());
            Assert.Equal(manuallySorted, automaticallySorted);
        }

        [Fact]
        public void TestSaveAndLoadFromJson()
        {
            var lib1 = new Library(); //an empty library
            var lib2 = new Library();
            AddSomeBooksToList(lib2); //a library with a list of some books
            Program.SaveLibrary(lib2); //saving lib2 in json
            lib1 = Program.LoadLibrary(); //loading from json into lib1

            var l1 = lib1.GetListOfBooks();
            var l2 = lib2.GetListOfBooks();

            for (int i = 0; i < l1.Count; i++)        //checking so that the elements in the lists in both libraries are equal
            {
                Assert.Equal(l1[i].title, l2[i].title);
                Assert.Equal(l1[i].author, l2[i].author);
                Assert.Equal(l1[i].ISBN, l2[i].ISBN);
                Assert.Equal(l1[i].category, l2[i].category);
            }
            Assert.Equal(l1.Count, l2.Count); //finally checking count to ensure l2 doesn't have more elements than l1
        }

        public (List<Book> manuallySortedList, List<Book> automaticallySortedList) AddSomeBooksToList(Library lib)
        {
            var b1 = lib.AddBook("Twilight", "Stephenie Meyer", "978-0-316-16017-9", "romance novel");
            var b2 = lib.AddBook("Frankenstein", "Mary Shelly", "978-0-8156-2640-4", "Gothic novel");
            var b3 = lib.AddBook("The Great Gatsby", "F. Scott Fitzgerald", "978-0-8108-9195-1", "novel");
            var b4 = lib.AddBook("Prince Caspian", "C. S. Lewis", "978-0-06-440500-3", "portal fantasy novel");
            var b5 = lib.AddBook("The Lion, the witch and the Wardrobe", "C. S. Lewis", "978-0-06-440942-1", "portal fantasy novel");
            var b6 = lib.AddBook("The Complete Chronicles of Narnia", "C. S. Lewis", "978-0-06-028137-3", "portal fantasy novel");
            var b7 = lib.AddBook("To Kill a Mockingbird", "Harper Lee", "978-0-8103-8566-5", "Gothic novel");

            var list = new List<Book>(); //create list and add in alphabetical order (author then title)
            list.Add(b4);
            list.Add(b6);
            list.Add(b5);
            list.Add(b3);
            list.Add(b7);
            list.Add(b2);
            list.Add(b1);

            return (list, lib.GetListOfBooks());
        }
    }
}
