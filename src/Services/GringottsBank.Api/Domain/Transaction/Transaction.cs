using GringottsBank.Api.Domain.Transaction.Enums;
using GringottsBank.Api.Domain.Transaction.InputModels;
using GringottsBank.Api.Infrastructure.ResponseModels;
using System;

namespace GringottsBank.Api.Domain.Transaction
{
    public class Transaction
    {
        private Transaction() { }

        public Guid AccountId { get; private set; }
        public Account.Account Account { get; set; }

        public Guid Id { get; private set; }
        public DateTime TransactionDateTime { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public decimal Amount { get; private set; }

        public static DomainResponse<Transaction> Create(AddTransactionInputModel parameters)
        {
            Transaction transaction = new Transaction
            {
                AccountId = parameters.AccountId,
                Amount = parameters.Amount,
                TransactionType = parameters.TransactionType,
                TransactionDateTime = DateTime.Now,
            };

            return DomainResponse<Transaction>.Ok(transaction);
        }
    }
}
