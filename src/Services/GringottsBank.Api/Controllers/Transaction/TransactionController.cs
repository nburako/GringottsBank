using GringottsBank.Api.Features.Transaction;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GringottsBank.Api.Controllers.Transaction
{
    public class TransactionController : Controller
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <remarks>
        /// TransactionType => 0: Withdraw, 1: Deposit
        /// </remarks>
        [HttpPost("/transaction")]
        public async Task<ActionResult<FeatureResponse<AddTransaction.ResponseDto>>> AddTransaction(AddTransaction.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}
