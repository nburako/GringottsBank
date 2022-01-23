using GringottsBank.Api.Infrastructure.Database;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GringottsBank.Api.Features.Account
{
    public class GetAccountTransactions
    {
        public record Query(Guid AccountId) : IRequest<FeatureResponse<ResponseDto>>;

        public class Handler : IRequestHandler<Query, FeatureResponse<ResponseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            public Handler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<FeatureResponse<ResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var account = await _dbContext.Accounts.Include(x => x.Transactions)
                                                       .FirstOrDefaultAsync(x => x.Id == request.AccountId);

                if (account is null)
                    return FeatureResponse<ResponseDto>.Fail("Account is not found.");

                return FeatureResponse<ResponseDto>.Ok(ResponseDto.BuildDto(account.Transactions.ToList()));
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
            public DateTime TransactionDateTime { get; set; }
            public string TransactionType { get; set; }
            public decimal Amount { get; set; }
        }
    }
}
