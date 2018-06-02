using Microsoft.EntityFrameworkCore;
using TaskApi.Entities;

namespace TaskApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<TaskEntity> TaskEntity { get; set; } // Should be Tasks

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
