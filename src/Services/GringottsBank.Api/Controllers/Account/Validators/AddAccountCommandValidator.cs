using FluentValidation;
using GringottsBank.Api.Features.Account;

namespace GringottsBank.Api.Controllers.Account.Validators
{
    public class AddAccountCommandValidator : AbstractValidator<AddAccount.Command>
    {
        public AddAccountCommandValidator()
        {
            RuleFor(c => c.Balance).NotEmpty().NotNull();
            RuleFor(c => c.CurrencyCode).NotEmpty().NotNull();
            RuleFor(c => c.CustomerId).NotEmpty().NotNull();
        }
    }
}
