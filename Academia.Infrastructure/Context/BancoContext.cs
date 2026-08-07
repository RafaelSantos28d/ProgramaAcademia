using Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Academia.Infrastructure.Context
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoContext).Assembly);
        }
        public DbSet<Student> Students {  get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Plan> Plans { get; set; }
    }
}
