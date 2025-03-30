using Microsoft.EntityFrameworkCore;
using CarInventory.Models;

namespace CarInventory.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
    }
}
