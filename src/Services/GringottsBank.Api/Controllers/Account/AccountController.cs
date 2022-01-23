using GringottsBank.Api.Features.Account;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GringottsBank.Api.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/account")]
        public async Task<ActionResult<FeatureResponse<AddAccount.ResponseDto>>> AddAccount(AddAccount.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("/details/{accountId}")]
        public async Task<ActionResult<FeatureResponse<GetAccountDetails.ResponseDto>>> GetAccountDetails(Guid accountId)
        {
            return await _mediator.Send(new GetAccountDetails.Query(accountId));
        }

        [HttpGet("/transactions/{accountId}")]
        public async Task<ActionResult<FeatureResponse<GetAccountTransactions.ResponseDto>>> GetAccountTransactions(Guid accountId)
        {
            return await _mediator.Send(new GetAccountTransactions.Query(accountId));
        }
    }
}
