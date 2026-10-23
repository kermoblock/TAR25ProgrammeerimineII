using ShopTARpe25.Core.Domain;

namespace ShopTARpe25.LINQ
{
    public class LINQSorting
    {
        public List<Book> FindBooksByYear(
            List<Book> books, int year)
        {
            return books
                .Where(x => x.PublishedYear == year)
                .ToList();
        }
        public List<Book> FindBooksTitle(
           List<Book> books, string pealkiri)
        {
            return books
                .Where(x => x.Title == pealkiri)
                .ToList();
        }
        public List<Book> UnavaliableBooks(
            List<Book> books)
        {
            return books
                .Where(x => x.IsAvaliable == false)
                .ToList();
        }
        public List<Loan> ReaderLoans(
             List<Loan> loans, Guid ReaderId)
        {
            return loans
                .Where(x => x.ReaderId == ReaderId)
                .ToList();
        }
    }
}
