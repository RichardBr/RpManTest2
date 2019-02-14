using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RpMan.Domain.Entities;
using RpMan.Persistence.Configurations;

namespace RpMan.Persistence
{
    public class RpManDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public RpManDbContext(DbContextOptions<RpManDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        // public DbSet<User> Users { get; set; }  // Not required anymore as it will come ASPNET CORE Identity

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RpManDbContext).Assembly);
        }
    }
}
