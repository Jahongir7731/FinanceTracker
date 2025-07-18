namespace FinanceTracker.Domain.Entities
{
    public class Income
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public Category Category { get; set; } = new();
        public User User { get; set; } = new();
        public bool IsDeleted { get; set; } = false;
    }

}
