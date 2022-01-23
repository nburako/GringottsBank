using GringottsBank.Api.Domain.Transaction.Enums;
using System;

namespace GringottsBank.Api.Domain.Transaction.InputModels
{
    public record AddTransactionInputModel
    {
        public Guid AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
