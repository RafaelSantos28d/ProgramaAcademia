using Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.EntitiesConfiguration
{
    internal class Studentconfiguration :IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250).IsRequired();
            builder.Property(x=>x.Email).HasMaxLength(270).IsRequired();
            builder.Property(x=> x.CPF).HasMaxLength(11).IsRequired(); 
            builder.Property(x=>x.Phone).HasMaxLength(10).IsRequired();
        }
    }
}
