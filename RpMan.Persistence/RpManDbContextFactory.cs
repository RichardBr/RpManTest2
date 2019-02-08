using Microsoft.EntityFrameworkCore;
using RpMan.Persistence.Infrastructure;

namespace RpMan.Persistence
{
    public class RpManDbContextFactory : DesignTimeDbContextFactoryBase<RpManDbContext>
    {
        protected override RpManDbContext CreateNewInstance(DbContextOptions<RpManDbContext> options)
        {
            return new RpManDbContext(options);
        }
    }
}
