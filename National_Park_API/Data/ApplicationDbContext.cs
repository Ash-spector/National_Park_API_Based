using Microsoft.EntityFrameworkCore;
using National_Park_API.Models;

namespace National_Park_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<National_Park> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
    }
}