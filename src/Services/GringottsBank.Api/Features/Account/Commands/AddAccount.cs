using GringottsBank.Api.Domain.Account.InputModels;
using GringottsBank.Api.Infrastructure.Database;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GringottsBank.Api.Features.Account
{
    public class AddAccount
    {
        public record Command() : AddAccountInputModel, IRequest<FeatureResponse<ResponseDto>>;

        public class Handler : IRequestHandler<Command, FeatureResponse<ResponseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            public Handler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<FeatureResponse<ResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId);

                if (customer is null)
                    return FeatureResponse<ResponseDto>.Fail("CustomerId is not valid.");

                var account = Domain.Account.Account.Create(request);
                customer.AddAccount(account.Result);
                
                await _dbContext.SaveChangesAsync();

                return FeatureResponse<ResponseDto>.Ok(ResponseDto.BuildDto(account.Result.Id));
            }
        }

        public record ResponseDto
        {
            public Guid CreatedAccountId { get; set; }

            public static ResponseDto BuildDto(Guid id)
            {
                return new ResponseDto()
                {
                    CreatedAccountId = id
                };
            }
        }
    }
}
