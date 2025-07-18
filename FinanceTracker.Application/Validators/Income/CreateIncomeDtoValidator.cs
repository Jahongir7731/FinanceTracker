using FinanceTracker.Application.DTOs.Income;
using FluentValidation;

namespace FinanceTracker.Application.Validators.Income
{
    public class CreateIncomeDtoValidator : AbstractValidator<CreateIncomeDto>
    {
        public CreateIncomeDtoValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }

}
