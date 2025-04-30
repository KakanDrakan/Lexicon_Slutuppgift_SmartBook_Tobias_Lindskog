A Library application as a final assignment for a course in basic C#.

How to use the program is hopefully self-explanatory by following the instructions in the console. You can add/remove books and users, and users can borrow/return books. You can also save and load libraries, and extract a report of borrowed books.

When adding users, remember the ID you give ithem because that is what's used to identify them later (borrowing books or removing them), but users can always be listed with IDs by typing 7 in the main menu.

Similarly, books are identified by either title or ISBN, and can be listed by typing 3.

A file called "LibraryLog.txt" logs when books and users are added/removed, and when books are borrowed/returned. The log can be cleared by tying 12.

Typing 11 in the menu extracts a report called "BorrowedBooksReport.txt". It is also printed in the console.



xUnit Tests:

Tested so that books are being sorted properly when added to the library by comparing a manually sorted and automatically sorted list of books.

Tested so that saving and loading from JSON works properly by adding two empty libraries, then adding books to one of them and saving that library. Then finally loading that library into the other library and checking id the books in them are the same.
