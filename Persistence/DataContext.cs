using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<PlayerDb>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CellDb> Cells { get; set; }
        public DbSet<ShipDb> Ships { get; set; }
        public DbSet<CellShipDb> CellShips { get; set; }
        public DbSet<FieldDb> Fields { get; set; }
        public DbSet<GameDb> Games { get; set; }
        public DbSet<PlayerDb> Players { get; set; }
        public DbSet<ShootDb> Shoots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GameDb>()
                .HasOne(p => p.FirstPlayerField)
                .WithMany(t => t.FirstPlayerGames)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<GameDb>()
                .HasOne(p => p.SecondPlayerField)
                .WithMany(t => t.SecondPlayerGames)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CellShipDb>()
                .HasOne(p => p.Cell)
                .WithMany(t => t.CellShips)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CellShipDb>()
                .HasOne(p => p.Field)
                .WithMany(t => t.CellShips)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<CellShipDb>()
                .HasOne(p => p.Ship)
                .WithMany(t => t.CellShips)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}