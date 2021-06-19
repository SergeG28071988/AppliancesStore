using System.Data.Entity;

namespace AppliancesStore.Models.Repository
{
    public class EFDbContext : DbContext
    {
        public DbSet<Appliance> Appliances { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
