using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SunsetsIO.Models;

namespace SunsetsIO.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Rating> Rating { get; set; }
        public DbSet<LocalWeather> LocalWeather { get; set; }
    }
}