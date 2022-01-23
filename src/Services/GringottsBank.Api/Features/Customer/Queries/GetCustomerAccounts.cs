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
    public class GetCustomerAccounts
    {
        public record Query(Guid CustomerId) : IRequest<FeatureResponse<ResponseDto>>;

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
                                                         .FirstOrDefaultAsync(x => x.Id == request.CustomerId);

                if (customer is null)
                    return FeatureResponse<ResponseDto>.Fail("Customer is not found.");

                return FeatureResponse<ResponseDto>.Ok(ResponseDto.BuildDto(customer.Accounts.ToList()));
            }
        }

        public record ResponseDto
        {
            public List<AccountResponseDto> AccountList { get; set; }

            public static ResponseDto BuildDto(List<Domain.Account.Account> accountList)
            {
                List<AccountResponseDto> list = new List<AccountResponseDto>();
                foreach (var account in accountList)
                {
                    var accountResponseDto = new AccountResponseDto
                    {
                        Id = account.Id,
                        AccountStatus = account.AccountStatus.ToString(),
                        Balance = account.Balance,
                        CurrencyCode = account.CurrencyCode,
                        OpeningDateTime = account.OpeningDateTime
                    };

                    list.Add(accountResponseDto);
                }

                return new ResponseDto { AccountList = list };
            }
        }

        public record AccountResponseDto
        {
            public Guid Id { get; set; }
            public DateTime OpeningDateTime { get; set; }
            public string CurrencyCode { get; set; }
            public decimal Balance { get; set; }
            public string AccountStatus { get; set; }
        }
    }
}
