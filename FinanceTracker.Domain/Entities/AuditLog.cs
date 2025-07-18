namespace FinanceTracker.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public Guid EntityId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
    }
}
