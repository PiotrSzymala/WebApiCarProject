using Microsoft.EntityFrameworkCore;
using WebApiCarProject.Infrastructure.Entities;

namespace WebApiCarProject.Infrastructure.DatabseContexts
{
    public class CarDbContext : DbContext
    {
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<User> Users { get; set; }


        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=aws-0-eu-central-1.pooler.supabase.com;Database=postgres;Username=postgres.wbasgoycajolaqvzlwcn;Password=*h9AB_AnBnJE&hS");
    }
}
