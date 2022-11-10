using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SunsetsIO.Models;

namespace SunsetsIO.Data
{
    public class AppDbContext : IdentityDbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        
        public DbSet<Rating> Rating { get; set; }
        public DbSet<LocalWeather> LocalWeather { get; set; }
    }
}