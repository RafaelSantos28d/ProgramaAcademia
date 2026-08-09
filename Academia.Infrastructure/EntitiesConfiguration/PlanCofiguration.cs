using Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.EntitiesConfiguration
{
    public class PlanCofiguration :IEntityTypeConfiguration<Plan>
    {
        public void Configure (EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x=>x.DurationDays).IsRequired();
            builder.Property(x=>x.Price).HasPrecision(10,2).IsRequired();
            builder.Property(x=>x.Name).HasMaxLength(200).IsRequired();
        }
    }
}
