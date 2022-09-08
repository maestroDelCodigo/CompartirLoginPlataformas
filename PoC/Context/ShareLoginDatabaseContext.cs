using Microsoft.EntityFrameworkCore;
using PoC.Models;

namespace PoC.Context
{
    public class ShareLoginDatabaseContext : DbContext
    {
        public ShareLoginDatabaseContext(DbContextOptions<ShareLoginDatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<AccessLogin> AccessLogin { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(p => p.AccessLogins)
                .WithMany(g => g.User)
                .UsingEntity<UserLogin>(
                    pg => pg.HasOne(prop => prop.AccessLogin)
                    .WithMany()
                    .HasForeignKey(prop => prop.IdAccessLogin),
                    pg => pg.HasOne(prop => prop.User)
                    .WithMany()
                    .HasForeignKey(prop => prop.IdUser)
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
