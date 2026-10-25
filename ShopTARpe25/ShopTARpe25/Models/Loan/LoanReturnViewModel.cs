namespace ShopTARpe25.Models.Loan
{
    public class LoanReturnViewModel
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public Guid ReaderId { get; set; }
        public string FullName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
