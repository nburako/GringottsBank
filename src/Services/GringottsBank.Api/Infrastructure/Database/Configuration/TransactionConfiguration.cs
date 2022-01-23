using GringottsBank.Api.Domain.Transaction;
using GringottsBank.Api.Domain.Transaction.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GringottsBank.Api.Infrastructure.Database.Configuration
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(e => e.TransactionType)
            .HasConversion(
                v => v.ToString(),
                v => (TransactionType)Enum.Parse(typeof(TransactionType), v))
                .IsUnicode(false)
                .IsRequired(true);
            builder.Property(e => e.TransactionDateTime).IsRequired(true);
            builder.Property(e => e.Amount).IsRequired(true);

            builder.HasOne(e => e.Account)
                .WithMany(e => e.Transactions)
                .IsRequired()
                .HasForeignKey(e => e.AccountId);
        }
    }
}
