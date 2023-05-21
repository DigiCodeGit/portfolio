using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Microsoft.AspNetCore.Identity;
using Portfolio.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// My generic sql handler
builder.Services.AddDbContext<PortfolioDbSql>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));

// Sql handler for Identity processes
builder.Services.AddDbContext<PortalDbSql>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection")));

builder.Services.AddDefaultIdentity<PortalIdentityUsers>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PortalDbSql>();

// Build
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
