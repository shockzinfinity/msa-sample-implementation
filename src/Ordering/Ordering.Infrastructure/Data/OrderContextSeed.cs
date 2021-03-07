using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
  public class OrderContextSeed
  {
    public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry  = 0)
    {
      int retryForAvailability = retry.Value;

      try
      {
        orderContext.Database.Migrate();

        if (!orderContext.Orders.Any())
        {
          orderContext.Orders.AddRange(GetPreconfiguredOrders());
          await orderContext.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        if(retryForAvailability < 3)
        {
          retryForAvailability++;
          var log = loggerFactory.CreateLogger<OrderContextSeed>();
          log.LogError(ex.Message);
          await SeedAsync(orderContext, loggerFactory, retryForAvailability);
        }
      }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
      return new List<Order>
      {
        new Order() {UserName = "shockz", FirstName = "Jun", LastName = "Yu", EmailAddress = "test@test.com", AddressLine = "365", Country="Korea"}
      };
    }
  }
}
