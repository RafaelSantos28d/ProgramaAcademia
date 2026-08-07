using Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.EntitiesConfiguration
{
    public class EnrollmentConfiguration :IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x=>x.StudentId).IsRequired();
            builder.Property(x=>x.PlanId).IsRequired();
            builder.HasOne(x => x.Plan).WithMany(x=>x.Enrollments).HasForeignKey(x=>x.PlanId);
            builder.HasOne(x => x.Student).WithMany(x => x.Enrollments).HasForeignKey(x=> x.StudentId);
        }
    }
}
