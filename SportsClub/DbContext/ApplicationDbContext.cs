namespace SportsClub.DbContext
{
    using Microsoft.EntityFrameworkCore;
    using SportsClub.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
