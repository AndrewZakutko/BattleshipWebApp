using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext
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
    }
}