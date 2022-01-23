using FluentValidation;
using GringottsBank.Api.Features.Customer;

namespace GringottsBank.Api.Controllers.Customer.Validators
{
    public class AddCustomerCommandValidator : AbstractValidator<AddCustomer.Command>
    {
        public AddCustomerCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
            RuleFor(c => c.Surname).NotEmpty().NotNull();
            RuleFor(c => c.BirthYear).NotEmpty().NotNull();
            RuleFor(c => c.Address).NotEmpty().NotNull();
            RuleFor(c => c.Email).NotEmpty().NotNull();
            RuleFor(c => c.MobilePhone).NotEmpty().NotNull();
        }
    }
}
