using System;

namespace ControlPaper2
{
    public abstract class Edition
    {
        private String isbn;
        
        private String title;

        private int yearOfPublishing;

        public int YearOfPublishing => yearOfPublishing;

        public string ISBN => isbn;

        public string Title => title;

        protected Edition(string isbn, string title, int yearOfPublishing)
        {
            this.isbn = isbn;
            this.title = title;
            this.yearOfPublishing = yearOfPublishing;
        }

        public override string ToString()
        {
            return String.Format("({0}, {1}, {2})", title, isbn, yearOfPublishing.ToString());
        }

        public virtual void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}