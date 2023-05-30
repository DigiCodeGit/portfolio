using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Areas.Identity.Data
{
    // Our translator to translate C# database "queries"/code to sql queries
    public class PortalECommerceDbSql: DbContext
    {
        // Pass sql options to base dbcontext class we inherited from
        public PortalECommerceDbSql(DbContextOptions<PortalECommerceDbSql> options) : base(options) 
        { 
        
        }

        // Define tables (C# side of things)
        public DbSet<Artwork> Artwork { get; set; }
    }
}
