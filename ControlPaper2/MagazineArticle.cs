using System;
using System.Collections.Generic;

namespace ControlPaper2
{
    public class MagazineArticle: IHasAuthors
    {
        private String title;
        
        private List<String> authors;

        public MagazineArticle(string title, List<string> authors)
        {
            this.title = title;
            this.authors = authors;
        }

        public List<string> Authors => authors;

        public override string ToString()
        {
            return $"Article({title})";
        }
    }
}