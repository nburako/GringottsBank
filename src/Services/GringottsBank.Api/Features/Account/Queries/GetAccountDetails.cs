using GringottsBank.Api.Infrastructure.Database;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GringottsBank.Api.Features.Account
{
    public class GetAccountDetails
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
                var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == request.AccountId);

                if (account is null)
                    return FeatureResponse<ResponseDto>.Fail("Account is not found.");


                return FeatureResponse<ResponseDto>.Ok(ResponseDto.BuildDto(account));
            }
        }

        public record ResponseDto
        {
            public Guid CustomerId { get; set; }
            public DateTime OpeningDateTime { get; set; }
            public string CurrencyCode { get; private set; }
            public decimal Balance { get; private set; }
            public string AccountStatus { get; private set; }

            public static ResponseDto BuildDto(Domain.Account.Account account)
            {
                return new ResponseDto()
                {
                    AccountStatus = account.AccountStatus.ToString(),
                    Balance = account.Balance,
                    CurrencyCode = account.CurrencyCode,
                    CustomerId = account.CustomerId,
                    OpeningDateTime = account.OpeningDateTime
                };
            }
        }
    }
}
