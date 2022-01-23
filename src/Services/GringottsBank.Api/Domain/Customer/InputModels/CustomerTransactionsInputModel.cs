using System;

namespace GringottsBank.Api.Domain.Customer.InputModels
{
    public record CustomerTransactionsInputModel
    {
        public Guid CustomerId { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }
}
