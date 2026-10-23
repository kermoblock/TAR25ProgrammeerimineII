namespace ShopTARpe25.Core.Domain
{
    public class Reader
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
