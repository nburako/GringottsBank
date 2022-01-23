using FluentValidation;
using GringottsBank.Api.Features.Customer;

namespace GringottsBank.Api.Controllers.Customer.Validators
{
    public class GetCustomerTransactionsValidator : AbstractValidator<GetCustomerTransactions.Query>
    {
        public GetCustomerTransactionsValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().NotNull();
            RuleFor(c => c.StartYear).NotEmpty().NotNull();
            RuleFor(c => c.EndYear).NotEmpty().NotNull();
        }
    }
}
