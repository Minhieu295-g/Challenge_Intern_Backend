using Microsoft.EntityFrameworkCore;

namespace Challenge2.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        #region Dbset

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        #endregion

        protected MyDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Address>()
                .HasIndex(a => a.UserId);
        }
    }
}
