using Microsoft.EntityFrameworkCore;
using Project1.Models.Domain;

namespace Project1.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Regions> Regions { get; set; }

        public DbSet<Walks> Walks { get; set; }

    }
}
