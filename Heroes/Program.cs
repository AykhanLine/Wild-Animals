using Heroes.Data;
using Heroes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>
    (opt => opt.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/dashboard/auth/login";
    options.AccessDeniedPath = "/dashboard/auth/login";
});

var app = builder.Build();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
    );


app.Run();
