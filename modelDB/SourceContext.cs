using Microsoft.EntityFrameworkCore;

namespace modelDB
{
    public class SourceContext : DbContext
    {
        public SourceContext(DbContextOptions<SourceContext> options)
            : base(options) {}

        public DbSet<Make> Makes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
    }
}
