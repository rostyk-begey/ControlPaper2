using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;

namespace ControlPaper2
{
    public class Library
    {
        public event BorrowHandler BorrowCompleted;

        private List<Edition> _editions;
            
        private double bookBasePrice;

        private double magazineBasePrice;

        public Library(List<Edition> editions, double bookBasePrice, double magazineBasePrice)
        {
            _editions = editions;
            this.bookBasePrice = bookBasePrice;
            this.magazineBasePrice = magazineBasePrice;
            magazinesPrestigeCoeficients = new Dictionary<String, int>();
            borrovedEditions = new HashSet<String>();
        }

        private Dictionary<String, int> magazinesPrestigeCoeficients;

        private HashSet<String> borrovedEditions;

        public List<Edition> Editions => _editions;

        public List<IHasAuthors> FindEditionsByAuthor(String searchedAuthor)
        {
            List<IHasAuthors> result = new List<IHasAuthors>();
            
            foreach (var edition in _editions)
            {

                var book = edition as Book;
                var magazine = edition as Magazine;
                
                if (edition is Book)
                {
                    if (book.Authors.Contains(searchedAuthor))
                    {
                        result.Add((IHasAuthors)edition);
                    }
                }
                else if (edition is Magazine)
                {
                    foreach (var article in magazine.Articles)
                    {
                        if (article.Authors.Contains(searchedAuthor))
                        {
                            result.Add((IHasAuthors)article);
                        }
                    }
                }
            }

            return result;
        }

        public double GetBorrowPrice(String editionIsbn)
        {
            var edition = _editions.Find(x => x.ISBN == editionIsbn);
            int currentYear = DateTime.Now.Year;
            int yearOfPublishing = edition.YearOfPublishing;

            if (edition is Book)
            {
                return bookBasePrice / (currentYear - yearOfPublishing + 1);
            }
            else if (edition is Magazine)
            {
                int prestigeCoefficient;
                if (magazinesPrestigeCoeficients.TryGetValue(editionIsbn, out prestigeCoefficient))
                {
                    if (prestigeCoefficient >= 1 && prestigeCoefficient <= 5)
                    {
                        return magazineBasePrice / (currentYear - yearOfPublishing + 1);
                    }
                }
                throw new Exception("ERROR: Wrong prestige coefficient");
            }
            
            throw new Exception("ERROR: Wrong edition type");
        }

        public void SetMagazinePrestigeCoefficient(String editionIsbn, int prestigeCoefficient)
        {
            var edition = _editions.Find(x => x.ISBN == editionIsbn);

            if (edition is Magazine)
            {
                if (prestigeCoefficient >= 1 && prestigeCoefficient <= 5)
                {
                    magazinesPrestigeCoeficients.Add(editionIsbn, prestigeCoefficient);
                }
                else
                {
                    throw new Exception("ERROR: Prestige coefficient must be in range from 1 to 5");
                }
            }
            else
            {
                throw new Exception($"ERROR: Edition ({editionIsbn}) is not a magazine");   
            }
        }

        public void Borrow(String editionIsbn, User user)
        {
            if (borrovedEditions.Contains(editionIsbn))
            {
                throw new Exception($"ERROR: Edition ({editionIsbn}) has already been borrowed");
            }

            var price = GetBorrowPrice(editionIsbn);
            borrovedEditions.Add(editionIsbn);
            BorrowCompleted?.Invoke($"User '{user.Name} {user.Surname}' has borrowed an edition {editionIsbn} at {DateTime.Now.ToString()}");
        }
    }
}