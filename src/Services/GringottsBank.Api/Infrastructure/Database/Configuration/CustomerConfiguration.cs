using GringottsBank.Api.Domain.Customer;
using GringottsBank.Api.Domain.Customer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GringottsBank.Api.Infrastructure.Database.Configuration
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(e => e.CustomerStatus)
            .HasConversion(
                v => v.ToString(),
                v => (CustomerStatus)Enum.Parse(typeof(CustomerStatus), v))
                .IsUnicode(false)
                .IsRequired(true);
            builder.Property(e => e.Address).IsRequired(true);
            builder.Property(e => e.BirthYear).IsRequired(true);
            builder.Property(e => e.Email).IsRequired(true);
            builder.Property(e => e.MobilePhone).IsRequired(true);
            builder.Property(e => e.Name).IsRequired(true);
            builder.Property(e => e.Surname).IsRequired(true);
        }
    }
}
