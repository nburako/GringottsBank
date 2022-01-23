using GringottsBank.Api.Domain.Transaction.InputModels;
using GringottsBank.Api.Infrastructure.Database;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GringottsBank.Api.Features.Transaction
{
    public class AddTransaction
    {
        public record Command() : AddTransactionInputModel, IRequest<FeatureResponse<ResponseDto>>;

        public class Handler : IRequestHandler<Command, FeatureResponse<ResponseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            public Handler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<FeatureResponse<ResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == request.AccountId);

                if (account is null)
                    return FeatureResponse<ResponseDto>.Fail("Account is not found.");

                var transaction = Domain.Transaction.Transaction.Create(request);
                var transactionResult = account.AddTransaction(transaction.Result);

                if (transactionResult.IsSuccess is false)
                    return FeatureResponse<ResponseDto>.Fail(transactionResult.ErrorMessage);

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return FeatureResponse<ResponseDto>.Fail("Concurrency error. Please try later.");
                }
                

                return FeatureResponse<ResponseDto>.Ok(ResponseDto.BuildDto(transaction.Result.Id));
            }
        }

        public record ResponseDto
        {
            public Guid CreatedTransactionId { get; set; }

            public static ResponseDto BuildDto(Guid id)
            {
                return new ResponseDto()
                {
                    CreatedTransactionId = id
                };
            }
        }
    }
}
