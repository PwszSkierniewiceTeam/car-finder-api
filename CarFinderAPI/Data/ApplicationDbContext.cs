using CarFinderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarFinderAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}