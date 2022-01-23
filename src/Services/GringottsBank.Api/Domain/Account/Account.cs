using GringottsBank.Api.Domain.Account.Enums;
using GringottsBank.Api.Domain.Account.InputModels;
using GringottsBank.Api.Domain.Transaction.Enums;
using GringottsBank.Api.Infrastructure.ResponseModels;
using System;
using System.Collections.Generic;

namespace GringottsBank.Api.Domain.Account
{
    public class Account
    {
        private Account() 
        {
            _transactions = new List<Transaction.Transaction>();
        }

        public Guid CustomerId { get; private set; }
        public Customer.Customer Customer { get; set; }

        public Guid Id { get; private set; }
        public DateTime OpeningDateTime { get; private set; }
        public string CurrencyCode { get; private set; }
        public decimal Balance { get; private set; }
        public AccountStatus AccountStatus { get; private set; }
        public uint xmin { get; private set; } // PostgreSQL Concurrency Token

        private List<Transaction.Transaction> _transactions { get; set; }
        public IReadOnlyCollection<Transaction.Transaction> Transactions => _transactions;

        public static DomainResponse<Account> Create(AddAccountInputModel parameters)
        {
            Account account = new Account
            {
                Balance = parameters.Balance,
                CurrencyCode = parameters.CurrencyCode,
                CustomerId = parameters.CustomerId,
                OpeningDateTime = DateTime.Now,
                AccountStatus = AccountStatus.Active
            };

            return DomainResponse<Account>.Ok(account);
        }

        public DomainResponse<Account> AddTransaction(Transaction.Transaction transaction)
        {
            if (AccountStatus is not AccountStatus.Active)
                return DomainResponse<Account>.Fail("Account status is not valid to run this.");

            if (transaction.TransactionType is TransactionType.Withdraw)
            {
                if (Balance < transaction.Amount)
                    return DomainResponse<Account>.Fail("Amount is not enough to withdraw.");

                Balance = Balance - transaction.Amount;
                _transactions.Add(transaction);
            }

            if (transaction.TransactionType is TransactionType.Deposit)
            {
                Balance = Balance + transaction.Amount;
                _transactions.Add(transaction);
            }

            return DomainResponse<Account>.Ok(this);
        }
    }
}
