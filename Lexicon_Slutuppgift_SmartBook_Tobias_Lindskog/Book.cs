using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift_SmartBook_Tobias_Lindskog
{
    public class Book
    {
        private string _title;
        private string _ISBN;
        private string _author;
        private string _category;

        public string title 
        {
            get { return _title; } 
            set
            {
                if (value != "") _title = value;
                else throw new Exception("Invalid title: cannot be empty");
            }
        }
        public string ISBN 
        {
            get { return _ISBN; } 
            set 
            {
                if (value.All(c => char.IsDigit(c) || c == '-')) _ISBN = value;
                else throw new Exception("Invalid ISBN: can only contain digits and hyphens");
            } 
        }
        public string author 
        {
            get { return _author; }
            set
            {
                if (value != "") _author = value;
                else throw new Exception("Invalid author: cannot be empty");
            } 
        }
        public string category 
        {
            get { return _category; }
            set
            {
                if (value != "") _category = value;
                else throw new Exception("Invalid category: cannot be empty");
            }
        }

        public User borrower { get; set; }

        public Book(string name, string author, string ISBN, string category)
        {
            _title = name;
            _author = author;
            _ISBN = ISBN;
            _category = category;
        }

        public Book() { }

        public override string ToString()
        {
            return string.Format("{0, -12}{1, -75} {2, -18}", $"{(borrower == null ? "(available)":"(borrowed)")}", $"{_author}'s {_category} {title}", $"ISBN: {_ISBN}");
        }

        public string ToStringWithoutISBN()
        {
            return $"{_author}'s {_category} {title}";
        }

        public string ToStringWithBorrower()
        {
            return string.Format("{0, -80} {1, -20}", $"{_author}'s {_category} {title}", $"User: {borrower.name} {borrower.id}");
        }

        public void Borrow(User user)
        {
            if (borrower != null) throw new Exception("The book has already been borrowed");
            else
            {
                borrower = user;
            }
        }

        public void Return()
        {
            if (borrower != null)
            {
                borrower = null;
            }
            else throw new Exception("This book was not borrowed to begin with");
        }
    }
}
