using System;
using System.Collections.Generic;

namespace ControlPaper2
{
    public class Book: Edition, IHasAuthors
    {
        private List<String> authors;

        public Book(string isbn, string title, int yearOfPublishing, List<string> authors) : base(isbn, title, yearOfPublishing)
        {
            this.authors = authors;
        }

        public List<String> Authors => authors;

        public override string ToString()
        {
            return "Book" + base.ToString();
        }
    }
}