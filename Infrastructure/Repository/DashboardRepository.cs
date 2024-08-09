using Application.Repository;
using Application.Requests.DashboardDto;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace Infrastructure.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        public DashboardRepository(AppDbContext context) {
            _context = context;
        }
        public async Task<List<ProductDto>> GetLowStockItemsAsync()
        {
            var lowStockItems = await _context.Products
                .Select(p => new ProductDto
                {
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Price = p.Price
                })
                .ToListAsync();
 
            return lowStockItems;
        }


        public async Task<List<SalesTrendDto>> GetMonthlySalesTrendsAsync()
        {
            return await _context.Orders.GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new SalesTrendDto
                {
                    Year = g.Key.Year.ToString(),
                    Month = g.Key.Month.ToString(),
                    Sales = g.Sum(o => o.OrderItems.Sum(oi => (double)(oi.Qty * (double)oi.Price - ((double)oi.Discount *(double) (oi.Qty * (double)oi.Price)/100))))
                }).ToListAsync();
        }

        public async Task<List<ProductDto>> GetTopSellingProductsAsync()
        {
            return await _context.Orders
              .SelectMany(order => order.OrderItems)
              .GroupBy(orderItem => orderItem.ProductId)
              .Select(group => new ProductDto
              {
                  Name = group.First().Product.Name,
                  Quantity = group.Sum(orderItem => orderItem.Qty),
                  Price = group.First().Product.Price
              }).OrderByDescending(product => product.Quantity)
              .Take(5)
              .ToListAsync();
        }

        public async Task<double> GetTotalInventoryValueAsync()
        {
           return await _context.Products.Select(p => (double)(p.Price * (decimal)p.Quantity)).SumAsync();
        }

        public async Task<List<GraphDataSeries>> GetRevenueProfitCost()
        {
            var data = new List<GraphDataSeries>();

            var orders = await _context.Orders
                .Where(x => x.OrderStatus.Contains("completed"))
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
            var groupedData = orders.GroupBy(o => o.UpdatedAt.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Revenue = g.Sum(o => o.OrderItems
                        .Sum(oi => (double)(oi.Qty * (double)oi.Price - ((double)oi.Discount * (double)(oi.Qty * (double)oi.Price) / 100)))),
                    Cost = g.Sum(o => o.OrderItems
                        .Sum(oi => (double)(oi.Qty * (double)oi.Product.Cost))),
                    Profit = g.Sum(o => o.OrderItems
                        .Sum(oi => (double)(oi.Qty * (double)oi.Price - ((double)oi.Discount * (double)(oi.Qty * (double)oi.Price) / 100))) - o.OrderItems.Sum(oi => (double)(oi.Qty * (double)oi.Product.Cost)))
                }).ToList();

            var revenueSeries = new GraphDataSeries
            {
                Id = "Revenue",
                Data = groupedData.Select(g => new GraphDataPoint
                {
                    X = GetMonthName(g.Month),
                    Y = g.Revenue
                }).ToList()
            };

            var costsSeries = new GraphDataSeries
            {
                Id = "Costs",
                Data = groupedData.Select(g => new GraphDataPoint
                {
                    X = GetMonthName(g.Month),
                    Y = g.Cost
                }).ToList()
            };

            var profitsSeries = new GraphDataSeries
            {
                Id = "Profits",
                Data = groupedData.Select(g => new GraphDataPoint
                {
                    X = GetMonthName(g.Month),
                    Y = g.Profit
                }).ToList()
            };

            data.Add(revenueSeries);
            data.Add(costsSeries);
            data.Add(profitsSeries);

            return data;
        }
        public async Task<List<GraphDataSeries>> GetDailyRevenueProfitCostForWeeks()
        {
            var data = new List<GraphDataSeries>();

            var orders = await _context.Orders
                .Where(x => x.OrderStatus.Contains("completed"))
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var groupedData = orders.GroupBy(o => new
            {
                WeekStart = o.UpdatedAt.Date.AddDays(-(int)o.UpdatedAt.DayOfWeek),
                Day = o.UpdatedAt.Date
            })
            .Select(g => new
            {
                WeekStart = g.Key.WeekStart,
                Day = g.Key.Day,
                Revenue = g.Sum(o => o.OrderItems
                    .Sum(oi => (double)(oi.Qty * (double)oi.Price - ((double)oi.Discount * (double)(oi.Qty * (double)oi.Price) / 100)))),
                Cost = g.Sum(o => o.OrderItems
                    .Sum(oi => (double)(oi.Qty * (double)oi.Product.Cost))),
                Profit = g.Sum(o => o.OrderItems
                    .Sum(oi => (double)(oi.Qty * (double)oi.Price - ((double)oi.Discount * (double)(oi.Qty * (double)oi.Price) / 100))) - o.OrderItems.Sum(oi => (double)(oi.Qty * (double)oi.Product.Cost)))
            }).ToList();

            var groupedByWeek = groupedData.GroupBy(g => g.WeekStart).ToList();

            foreach (var weekGroup in groupedByWeek)
            {
                var revenueSeries = new GraphDataSeries
                {
                    Id = $"Revenue ",
                    Data = weekGroup.OrderBy(g => g.Day).Select(g => new GraphDataPoint
                    {
                        X = g.Day.ToString("MM/dd"),
                        Y = g.Revenue
                    }).ToList()
                };

                var costsSeries = new GraphDataSeries
                {
                    Id = $"Costs ",
                    Data = weekGroup.OrderBy(g => g.Day).Select(g => new GraphDataPoint
                    {
                        X = g.Day.ToString("MM/dd"),
                        Y = g.Cost
                    }).ToList()
                };

                var profitsSeries = new GraphDataSeries
                {
                    Id = $"Profits ",
                    Data = weekGroup.OrderBy(g => g.Day).Select(g => new GraphDataPoint
                    {
                        X = g.Day.ToString("MM/dd"),
                        Y = g.Profit
                    }).ToList()
                };

                data.Add(revenueSeries);
                data.Add(costsSeries);
                data.Add(profitsSeries);
            }

            return data;
        }


        private string GetMonthName(int month)
        {
            return new DateTime(1, month, 1).ToString("MMMM");
        }
    }
}
