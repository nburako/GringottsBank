using FluentValidation;
using GringottsBank.Api.Features.Account;

namespace GringottsBank.Api.Controllers.Account.Validators
{
    public class GetAccountTransactionsValidator : AbstractValidator<GetAccountTransactions.Query>
    {
        public GetAccountTransactionsValidator()
        {
            RuleFor(c => c.AccountId).NotEmpty().NotNull();
        }
    }
}
