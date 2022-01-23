using GringottsBank.Api.Domain.Account.Enums;
using GringottsBank.Api.Domain.Customer.InputModels;
using GringottsBank.Api.Infrastructure.Database;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GringottsBank.Api.Features.Customer
{
    public class GetCustomerTransactions
    {
        public record Query() : CustomerTransactionsInputModel, IRequest<FeatureResponse<ResponseDto>>;

        public class Handler : IRequestHandler<Query, FeatureResponse<ResponseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            public Handler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<FeatureResponse<ResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = await _dbContext.Customers.Include(x => x.Accounts)
                                                         .ThenInclude(x => x.Transactions)
                                                         .FirstOrDefaultAsync(x => x.Id == request.CustomerId);

                if (customer is null)
                    return FeatureResponse<ResponseDto>.Fail("Customer is not found.");

                var transactionList = customer.Accounts
                    .SelectMany(x => x.Transactions)
                    .Where(x => x.TransactionDateTime.Year <= request.EndYear && x.TransactionDateTime.Year >= request.StartYear)
                    .ToList();

                return FeatureResponse<ResponseDto>.Ok(ResponseDto.BuildDto(transactionList));
            }
        }

        public record ResponseDto
        {
            public List<TransactionResponseDto> TransactionList { get; set; }

            public static ResponseDto BuildDto(List<Domain.Transaction.Transaction> transactionList)
            {
                List<TransactionResponseDto> list = new List<TransactionResponseDto>();
                foreach (var transaction in transactionList)
                {
                    var transactionResponseDto = new TransactionResponseDto
                    {
                        Id = transaction.Id,
                        AccountId = transaction.AccountId,
                        Amount = transaction.Amount,
                        TransactionDateTime = transaction.TransactionDateTime,
                        TransactionType = transaction.TransactionType.ToString()
                    };

                    list.Add(transactionResponseDto);
                }

                return new ResponseDto { TransactionList = list };
            }
        }

        public record TransactionResponseDto
        {
            public Guid Id { get; set; }
            public Guid AccountId { get; set; }
            public DateTime TransactionDateTime { get; set; }
            public string TransactionType { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
