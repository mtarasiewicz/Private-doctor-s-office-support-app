using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;


namespace DocHub.Infrastructure.DbContext
{
    /// <summary>
    /// A class representing a database context inheriting from IdentityDbContext
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Patient> Patients { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Patient>().ToTable("Patients");
        }
    }
}
