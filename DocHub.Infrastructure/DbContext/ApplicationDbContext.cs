using DocHub.Core.Domain.Entities;
using DocHub.Core.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;


namespace DocHub.Infrastructure.DbContext
{
    /// <summary>
    /// A class representing a database context inheriting from IdentityDbContext
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Patient>().ToTable("Patients");
            builder.Entity<Appointment>().ToTable("Appointments");
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole()
            {
                Id = Guid.Parse("8FC5BB05-B154-4B14-97B5-CAA6BE775820"),
                Name = "Admin",
                NormalizedName = "ADMIN".ToUpper()
            });
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole()
            {
                Id = Guid.Parse("776FF189-6D50-4C1C-AA39-120007F40BC9"),
                Name = "Patient",
                NormalizedName = "PATIENT".ToUpper(),
            });
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = Guid.Parse("56123F55-83E3-4D3D-B59F-8A5E250F817E"),
                FirstName = "Administrator",
                LastName = "Administrator",
                Email = "admin@localhost",
                NormalizedEmail = "ADMIN@LOCALHOST".ToUpper(),
                UserName = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                PasswordHash = hasher.HashPassword(null, "Admin123"),
                SecurityStamp = "89A60FE0-0F8A-477D-9953-720E8DA36B73"
            });
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser()
            {
                Id = Guid.Parse("10F2BA6B-D114-4A9A-AC8F-BF1EDBAB4135"),
                FirstName = "Jan",
                LastName = "Kowalski",
                Email = "user@localhost",
                NormalizedEmail = "USER@LOCALHOST",
                UserName = "user@localhost",
                NormalizedUserName = "USER@LOCALHOST",
                PasswordHash = hasher.HashPassword(null,"User123"),
                SecurityStamp = "582DC540-AAD8-4544-978B-847059FEE03E"
            });
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("8FC5BB05-B154-4B14-97B5-CAA6BE775820"),
                    UserId = Guid.Parse("56123F55-83E3-4D3D-B59F-8A5E250F817E")
                });
            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>()
                {
                    RoleId = Guid.Parse("776FF189-6D50-4C1C-AA39-120007F40BC9"),
                    UserId = Guid.Parse("10F2BA6B-D114-4A9A-AC8F-BF1EDBAB4135"),
                });
            builder.Entity<Patient>().HasData(new Patient()
            {
                Id = Guid.Parse("F2EBB7EA-7E18-4B8D-BB41-A9F0D20722DF"),
                FirstName = "Jan",
                LastName = "Kowalski",
                UserId = Guid.Parse("10F2BA6B-D114-4A9A-AC8F-BF1EDBAB4135"),
                Email = "user@localhost"
            });

        }
    }
}
