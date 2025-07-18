namespace FinanceTracker.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = "#FFFFFF";
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }

}
