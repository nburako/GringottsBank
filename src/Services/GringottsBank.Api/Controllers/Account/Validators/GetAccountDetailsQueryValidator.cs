using FluentValidation;
using GringottsBank.Api.Features.Account;

namespace GringottsBank.Api.Controllers.Account.Validators
{
    public class GetAccountDetailsValidator : AbstractValidator<GetAccountDetails.Query>
    {
        public GetAccountDetailsValidator()
        {
            RuleFor(c => c.AccountId).NotEmpty().NotNull();
        }
    }
}
