using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Contexts;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath("/Users/lucaswictor/RiderProjects/Silicon-ASPNET/AspNetCore_MVC/") // Absolute path
            
            // relative path
            // .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AspNetCore_MVC"))
            
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var builder = new DbContextOptionsBuilder<DataContext>();
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        builder.UseNpgsql(connectionString);

        return new DataContext(builder.Options);
    }
}

