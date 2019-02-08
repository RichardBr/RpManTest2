using Microsoft.EntityFrameworkCore;
using RpMan.Domain.Entities;
using RpMan.Persistence.Configurations;

namespace RpMan.Persistence
{
    public class RpManDbContext : DbContext
    {
        public RpManDbContext(DbContextOptions<RpManDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RpManDbContext).Assembly);
        }
    }
}
