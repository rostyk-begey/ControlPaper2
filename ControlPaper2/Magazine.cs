using System;
using System.Collections.Generic;

namespace ControlPaper2
{
    public class Magazine: Edition
    {
        private List<MagazineArticle> articles;

        public Magazine(string isbn, string title, int yearOfPublishing, List<MagazineArticle> articles) : base(isbn, title, yearOfPublishing)
        {
            this.articles = articles;
        }

        public List<MagazineArticle> Articles => articles;
        
        public override string ToString()
        {
            return "Magazine" + base.ToString();
        }
    }
}