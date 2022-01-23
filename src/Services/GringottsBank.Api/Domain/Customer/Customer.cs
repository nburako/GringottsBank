using GringottsBank.Api.Domain.Customer.Enums;
using GringottsBank.Api.Domain.Customer.InputModels;
using GringottsBank.Api.Infrastructure.ResponseModels;
using System;
using System.Collections.Generic;

namespace GringottsBank.Api.Domain.Customer
{
    public class Customer
    {
        private Customer()
        {
            _accounts = new List<Account.Account>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Address { get; private set; }
        public string MobilePhone { get; private set; }
        public string Email { get; private set; }
        public ushort BirthYear { get; private set; }
        public CustomerStatus CustomerStatus { get; private set; }

        private List<Account.Account> _accounts { get; set; }
        public IReadOnlyCollection<Account.Account> Accounts => _accounts;

        public static DomainResponse<Customer> Create(AddCustomerInputModel parameters)
        {
            Customer customer = new Customer
            {
                Name = parameters.Name,
                Surname = parameters.Surname,
                Address = parameters.Address,
                BirthYear = parameters.BirthYear,
                MobilePhone = parameters.MobilePhone,
                Email = parameters.Email,
                CustomerStatus = CustomerStatus.Active
            };

            return DomainResponse<Customer>.Ok(customer);
        }

        public DomainResponse<Customer> AddAccount(Domain.Account.Account account)
        {
            if (CustomerStatus is CustomerStatus.Active)
            {
                _accounts.Add(account);
                return DomainResponse<Customer>.Ok(this);
            }
            else
            {
                return DomainResponse<Customer>.Fail("Customer status is not valid to run this.");
            }
        }
    }
}
