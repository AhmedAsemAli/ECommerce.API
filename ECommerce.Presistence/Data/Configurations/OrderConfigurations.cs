using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.SubTotal)
                .HasColumnType("decimal(8,2)");

            builder.OwnsOne(x => x.Address, OE =>
            {
                OE.Property(x => x.FirstName).HasMaxLength(50);
                OE.Property(x => x.LastName).HasMaxLength(50);
                OE.Property(x => x.Street).HasMaxLength(50);
                OE.Property(x => x.City).HasMaxLength(50);
                OE.Property(x => x.Country).HasMaxLength(50);
            });


        }
    }
}
