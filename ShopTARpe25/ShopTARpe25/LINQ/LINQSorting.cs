using ShopTARpe25.Core.Domain;

namespace ShopTARpe25.LINQ
{
    public class LINQSorting
    {
        public List<Book> FindBooksByYear
            (List<Book> books, string year)
        {
            return books
                .Where(x => x.PublishedYear.ToString().Contains(year))
                .ToList();
        }

        public List<Book> FindBooksTitle
            (List<Book> books, string pealkiri)
        {
            return books
                .Where(x => x.Title.ToLower().Contains(pealkiri.ToLower()))
                .ToList();
        }

        public List<Book> UnavaliableBooks
            (List<Book> books)
        {
            return books
                .Where(x => !x.IsAvaliable)
                .ToList();
        }

        public List<Loan> ReaderLoans
            (List<Loan> loans, Guid readerId)
        {
            return loans
                .Where(x => x.ReaderId == readerId)
                .ToList();
        }
    }
}
