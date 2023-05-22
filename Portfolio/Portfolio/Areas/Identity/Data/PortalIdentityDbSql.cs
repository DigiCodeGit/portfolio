using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.Identity.Data;

namespace Portfolio.Areas.Identity.Data;

public class PortalIdentityDbSql : IdentityDbContext<PortalIdentityUsers>
{
    public PortalIdentityDbSql(DbContextOptions<PortalIdentityDbSql> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
