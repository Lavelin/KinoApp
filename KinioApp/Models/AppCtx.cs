using KinioApp.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KinioApp.Models
{
    public class AppCtx : IdentityDbContext<User>
    {
        public AppCtx(DbContextOptions<AppCtx> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<GenreKino> GenresKino { get; set; }

    }
}  
