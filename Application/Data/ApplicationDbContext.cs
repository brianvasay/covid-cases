using Application.Data.Configurations;
using Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Case> Cases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // The following will enable the model builder object to create the schema using specified configuration objects.
            modelBuilder.ApplyConfiguration(new CaseConfiguration());
        }
    }
}
