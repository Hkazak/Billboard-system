using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.ModelConfigurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Order)
            .WithMany()
            .HasForeignKey(e => e.OrderId);
        builder.HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey(e => e.PaymentStatusId);
    }
}