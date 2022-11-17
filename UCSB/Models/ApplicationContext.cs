using Microsoft.EntityFrameworkCore;

namespace UCSB.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Letter> Letters { get; set; } = null!;
        public ApplicationContext()
        {
            Database.Migrate();

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=vklettersdb;Username=postgres;Password=admin");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Letter>().ToTable(ToDB.Domain);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
