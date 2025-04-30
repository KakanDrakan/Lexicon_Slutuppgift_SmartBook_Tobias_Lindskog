using Microsoft.Testing.Platform.Extensions.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift_SmartBook_Tobias_Lindskog
{
    public class User
    {
        
        private string _name;
        private string _id;
        
        public string name {
            get { return _name; }
            set
            {
                if (value != "") _name = value;
                else throw new Exception("Invalid name: cannot be empty");
            }
        }
        public string id
        {
            get { return _id; }
            set
            {
                if (value.All(c => char.IsDigit(c))) _id = value;
                else throw new Exception("Invalid id: can only contain digits");
            }
        }

        public List<Book> borrowedBooks { get; set; } = new List<Book>();
        public User()
        {
            
        }

        public User(string name, string id, string phone)
        {
            _name = name;
            _id = id;
        }

        public override string ToString()
        {
            return $"{name}({id})";
        }
    }
}
