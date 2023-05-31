using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.Identity.Data;
using Portfolio.Data;
using Portfolio.Data.Services;

var builder = WebApplication.CreateBuilder(args);
var databaseConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Identity db
builder.Services.AddDbContext<PortalIdentityDbSql>(options =>
    options.UseSqlServer(databaseConnection));

builder.Services.AddDefaultIdentity<PortalIdentityUsers>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PortalIdentityDbSql>();

// eCommerce db
builder.Services.AddDbContext<PortalECommerceDbSql>(options =>
    options.UseSqlServer(databaseConnection));

// eCommerce Service
// Scoped service will last until user session ends;
// Singleton will last until server ends (i.e. same variable/value for every user);
// Transient will be renew every use
builder.Services.AddScoped<IEComService,ECommerceService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Additional Settings
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequiredLength = 6;

    // Default Lockout settings.
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
