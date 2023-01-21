using Microsoft.EntityFrameworkCore;
using OptimisticConcurrencyInterception.Data;

namespace OptimisticConcurrencyInterception;

internal partial class Program
{
    static void Main(string[] args)
    {
        using (var context = new CustomerContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.AddRange(new Customer { Name = "Bill" }, new Customer { Name = "Bob" });

            context.SaveChanges();
        }

        using (var context1 = new CustomerContext())
        {
            var customer1 = context1.Customers.Single(e => e.Name == "Bill");

            using (var context2 = new CustomerContext())
            {
                var customer2 = context1.Customers.Single(e => e.Name == "Bill");
                context2.Entry(customer2).State = EntityState.Deleted;
                context2.SaveChanges();
            }

            context1.Entry(customer1).State = EntityState.Deleted;
            context1.SaveChanges();
        }

        AnsiConsole.MarkupLine("[yellow]Press ENTER to exit[/]");
        Console.ReadLine();
    }
}