using System;

namespace GringottsBank.Api.Domain.Account.InputModels
{
    public record AddAccountInputModel
    {
        public Guid CustomerId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Balance { get; set; }
    }
}
