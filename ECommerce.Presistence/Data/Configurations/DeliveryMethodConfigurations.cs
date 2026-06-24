using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistence.Data.Configurations
{
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x=>x.Price)
                .HasColumnType("decimal(8,2)");

            builder.Property(x => x.ShortName).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.DeliveryTime).HasMaxLength(50);
        }
    }
}
