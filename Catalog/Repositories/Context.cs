using Microsoft.EntityFrameworkCore;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Food> Foods => Set<Food>();
        public DbSet<Cuisine> Cuisines => Set<Cuisine>();
    }
}
