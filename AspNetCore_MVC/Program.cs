using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(x => x.LowercaseUrls = true);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserRepository>();
//builder.Services.AddScoped<FeatureRepository>();
//builder.Services.AddScoped<FeatureItemRepository>();

//builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<UserService>();



builder.Logging.AddConsole(); // Adds console logging
builder.Logging.AddDebug(); // Adds debug window logging

var app = builder.Build();

// Configure middleware
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();