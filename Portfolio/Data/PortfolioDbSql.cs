using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.Identity.Data;
using Portfolio.Models;  // Reference our Users class

namespace Portfolio.Data
{
    public class PortfolioDbSql: DbContext  // Inherit DbContext functionality
    {
        // Constructor
        public PortfolioDbSql(DbContextOptions<PortfolioDbSql> options) : base(options) 
        { 

        }

        // Settings to let NuGet Package Manager to create a table for us
        public DbSet<Users> Users { get; set; } // Set/Use columns from users class, and create Users table
 
    }
}
