using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OptimisticConcurrencyInterception.Interceptors;
using OptimisticConcurrencyInterception.Models;

namespace OptimisticConcurrencyInterception.Data;

public class CustomerContext : DbContext
{
    private static readonly SuppressDeleteConcurrencyInterceptor _concurrencyInterceptor = new();

    public DbSet<Customer> Customers
        => Set<Customer>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .AddInterceptors(_concurrencyInterceptor)
            .UseSqlite("Data Source = customers.db")
            .LogTo(Console.WriteLine, LogLevel.Information);
}