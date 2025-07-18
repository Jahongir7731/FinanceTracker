using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; } = false;

        public byte[] RowVersion { get; set; } = new byte[8]; 
        public Category? Category { get; set; }
    }

}
