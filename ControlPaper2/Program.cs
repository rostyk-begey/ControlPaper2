using System;
using System.Collections.Generic;

namespace ControlPaper2
{
    public delegate void BorrowHandler(String message);
    internal class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Execute();
        }
        public void Execute()
        {
            List<String> authors1 = new List<String>();
            authors1.Add("Author11");
            authors1.Add("Author12");
            authors1.Add("Author13");
            authors1.Add("Author14");
            
            List<String> authors2 = new List<String>();
            authors2.Add("Author21");
            authors2.Add("Author22");
            authors2.Add("Author23");
            authors2.Add("Author24");
            
            List<MagazineArticle> articles1 = new List<MagazineArticle>();
            articles1.Add(new MagazineArticle("Article11", authors1));
            articles1.Add(new MagazineArticle("Article12", authors2));
            
            List<MagazineArticle> articles2 = new List<MagazineArticle>();
            articles2.Add(new MagazineArticle("Article21", authors1));
            articles2.Add(new MagazineArticle("Article22", authors2));
            
            List<Edition> editions = new List<Edition>();
            editions.Add(new Book("111", "Book1", 2001, authors1));
            editions.Add(new Book("222", "Book2", 2002, authors2));
            editions.Add(new Magazine("333", "Magazine1", 2003, articles1));
            editions.Add(new Magazine("444", "Magazine2", 2004, articles2));
            
            Library library = new Library(editions, 2, 3);
            library.SetMagazinePrestigeCoefficient("333", 2);
            library.BorrowCompleted += DisplayMessage;
            
            User user1 = new User("user", "1");
            User user2 = new User("user", "2");
            
            /* TASK 1 */
            String[] borrowList = {"111", "333", "444"};
            double price = 0;

            foreach (var editionIsbn in borrowList)
            {
                try
                {
                    price += library.GetBorrowPrice(editionIsbn);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine($"\nTASK 1: Total price {price}");

           /* TASK 2 */
           Console.WriteLine("\nTASK 2");
           try
           {
               var foundEditions = library.FindEditionsByAuthor("Author11");
               foreach (var edition in foundEditions)
               {
                   Console.WriteLine(edition.ToString());
               }
           }
           catch (Exception e)
           {
               Console.WriteLine(e.Message);
           }
           
           /* TASK 3 */
           library.Borrow("111", user1);
           Console.WriteLine("\nTASK 3");
           foreach (var editionIsbn in borrowList)
           {
               try
               {
                   library.Borrow(editionIsbn, user2);
               }
               catch (Exception e)
               {
                   Console.WriteLine(e.Message);
               }
           }
        }
        
        private static void DisplayMessage(string message)
        {
            Console.WriteLine($"LOG: {message}");
        }
    }
}