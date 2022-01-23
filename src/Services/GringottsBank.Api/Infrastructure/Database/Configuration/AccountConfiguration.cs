using GringottsBank.Api.Domain.Account;
using GringottsBank.Api.Domain.Account.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GringottsBank.Api.Infrastructure.Database.Configuration
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(p => p.Id);
            builder.UseXminAsConcurrencyToken();

            builder.Property(e => e.AccountStatus)
            .HasConversion(
                v => v.ToString(),
                v => (AccountStatus)Enum.Parse(typeof(AccountStatus), v))
                .IsUnicode(false)
                .IsRequired(true);
            builder.Property(e => e.Balance).IsRequired(true);
            builder.Property(e => e.CurrencyCode).IsRequired(true);
            builder.Property(e => e.CustomerId).IsRequired(true);
            builder.Property(e => e.OpeningDateTime).IsRequired(true);

            builder.HasOne(e => e.Customer)
                .WithMany(e => e.Accounts)
                .IsRequired()
                .HasForeignKey(e => e.CustomerId);
        }
    }
}
