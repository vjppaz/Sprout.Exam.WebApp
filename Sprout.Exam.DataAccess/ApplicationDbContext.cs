using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.Common.Models;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasKey(m => m.Id);

            builder.Entity<Employee>()
                .Property(m => m.TIN)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Employee>()
                .Property(m => m.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Entity<Employee>()
                .HasOne(m => m.EmployeeType)
                .WithMany()
                .HasForeignKey(m => m.EmployeeTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<EmployeeType>().HasKey(m => m.Id);
            builder.Entity<EmployeeType>()
                .Property(m => m.TypeName)
                .HasMaxLength(50)
                .IsRequired();

            base.OnModelCreating(builder);
        }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }
    }
}
