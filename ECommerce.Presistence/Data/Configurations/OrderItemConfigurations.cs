using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistence.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Price)
                .HasColumnType("decimal(8,2)");

            builder.OwnsOne(x => x.Product, OE =>
            {
                OE.Property(x => x.ProductName).HasMaxLength(100);
                OE.Property(x => x.PictureUrl).HasMaxLength(100);
            });
        }
    }
}
