namespace FinanceTracker.Application.DTOs.Income
{
    public class UpdateIncomeDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public DateTime IncomeDate { get; set; }
    }

}
