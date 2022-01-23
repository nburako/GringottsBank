using FluentValidation;
using GringottsBank.Api.Features.Transaction;

namespace GringottsBank.Api.Controllers.Transaction.Validators
{
    public class AddTransactionCommandValidator : AbstractValidator<AddTransaction.Command>
    {
        public AddTransactionCommandValidator()
        {
            RuleFor(c => c.AccountId).NotEmpty().NotNull();
            RuleFor(c => c.Amount).NotEmpty().NotNull();
            RuleFor(c => c.TransactionType).NotNull();
        }
    }
}
