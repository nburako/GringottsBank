using GringottsBank.Api.Features.Customer;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GringottsBank.Api.Controllers.Customer
{
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/customer")]
        public async Task<ActionResult<FeatureResponse<AddCustomer.ResponseDto>>> AddCustomer(AddCustomer.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("/accounts/{customerId}")]
        public async Task<ActionResult<FeatureResponse<GetCustomerAccounts.ResponseDto>>> GetCustomerAccounts(Guid customerId)
        {
            return await _mediator.Send(new GetCustomerAccounts.Query(customerId));
        }

        [HttpPost("/transactions")]
        public async Task<ActionResult<FeatureResponse<GetCustomerTransactions.ResponseDto>>> GetCustomerTransactions(GetCustomerTransactions.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}
