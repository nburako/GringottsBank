using GringottsBank.Api.Domain.Customer.InputModels;
using GringottsBank.Api.Infrastructure.Database;
using GringottsBank.Api.Infrastructure.ResponseModels;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GringottsBank.Api.Features.Customer
{
    public class AddCustomer
    {
        public record Command() : AddCustomerInputModel, IRequest<FeatureResponse<ResponseDto>>;

        public class Handler : IRequestHandler<Command, FeatureResponse<ResponseDto>>
        {
            private readonly ApplicationDbContext _dbContext;
            public Handler(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<FeatureResponse<ResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var customer = Domain.Customer.Customer.Create(request);
                
                _dbContext.Customers.Add(customer.Result);
                await _dbContext.SaveChangesAsync();

                return FeatureResponse<ResponseDto>.Ok(ResponseDto.BuildDto(customer.Result.Id));
            }
        }

        public record ResponseDto
        {
            public Guid CreatedCustomerId { get; set; }

            public static ResponseDto BuildDto(Guid id)
            {
                return new ResponseDto()
                {
                    CreatedCustomerId = id
                };
            }
        }
    }
}
