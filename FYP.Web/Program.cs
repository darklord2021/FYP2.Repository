using FYP.DB.Context;
using FYP.Services;
using FYP.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<FYPContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FYPData")).EnableSensitiveDataLogging());

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Inside ConfigureServices method in Startup.cs
builder.Services.AddScoped<PDF_Generator>();



var app = builder.Build();

using var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//var host= Host.CreateDefaultBuilder(args)
    //.ConfigureLogging

// Create roles
if(!await roleManager.RoleExistsAsync("Admin"))
{ 
    var roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
}
if (!await roleManager.RoleExistsAsync("Customer"))
{
    var roleResult2 = await roleManager.CreateAsync(new IdentityRole("Customer"));
}
if (!await roleManager.RoleExistsAsync("Purchase"))
{
    var roleResult3 = await roleManager.CreateAsync(new IdentityRole("Purchase"));
}
if (!await roleManager.RoleExistsAsync("Sales"))
{
    var roleResult4 = await roleManager.CreateAsync(new IdentityRole("Sales"));
}
if(!await roleManager.RoleExistsAsync("Inventory"))
{
    var roleResult5 = await roleManager.CreateAsync(new IdentityRole("Inventory"));
}
if (!await roleManager.RoleExistsAsync("Finance")) 
{
    var roleResult6 = await roleManager.CreateAsync(new IdentityRole("Finance")); 
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
