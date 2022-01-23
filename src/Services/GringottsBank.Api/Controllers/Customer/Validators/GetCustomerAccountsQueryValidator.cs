using FluentValidation;
using GringottsBank.Api.Features.Customer;

namespace GringottsBank.Api.Controllers.Customer.Validators
{
    public class GetCustomerAccountsValidator : AbstractValidator<GetCustomerAccounts.Query>
    {
        public GetCustomerAccountsValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().NotNull();
        }
    }
}
