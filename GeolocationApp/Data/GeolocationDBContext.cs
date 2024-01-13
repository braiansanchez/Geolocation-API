using GeolocationApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeolocationApp.Data
{
    public class GeolocationDBContext : DbContext
    {
        public DbSet<UserDB> Users {  get; set; } 
        public DbSet<LocationDB> Locations {  get; set; }

        public GeolocationDBContext(DbContextOptions<GeolocationDBContext> options)
            : base(options) { }

    }
}
