using Auctions.Data;
using Auctions.Data.Services;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

//var _logStream = new StreamWriter("auctionlogs.txt", append: true);

if(builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options => {
        options.UseInMemoryDatabase("AuctionsDatabase"); 
        //options.LogTo(_logStream.WriteLine, Microsoft.Extensions.Logging.LogLevel.Trace);
        //options.EnableSensitiveDataLogging();
    }); 
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IListingsService, ListingsService>();
builder.Services.AddScoped<IBidsService, BidsService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();


if(app.Environment.IsDevelopment())
{   
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using var scope = scopeFactory.CreateScope();

    DatabaseSeeder.Seed(scope.ServiceProvider); 
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
