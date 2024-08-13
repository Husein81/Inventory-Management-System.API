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
            var year = DateTime.Now.Year;
            // Fetch completed orders with related data for the specified year
            var orders = await _context.Orders
                .Where(x => x.OrderStatus == "completed" && x.UpdatedAt.Year == year )
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            // Group orders by each month of the specified year
            var groupedData = orders.GroupBy(o => o.UpdatedAt.Month)
            .Select(g => new
            {
                Month = g.Key,
                Revenue = g.Sum(o => o.OrderItems
                    .Sum(oi => (oi.Qty * (double)oi.Price - ((double)oi.Discount * oi.Qty * (double)oi.Price / 100)))),
                Cost = g.Sum(o => o.OrderItems
                    .Sum(oi => (double)(oi.Qty * (double)oi.Product.Cost))),
                Profit = g.Sum(o => o.OrderItems
                    .Sum(oi => (double)(oi.Qty * (double)oi.Price - ((double)oi.Discount * oi.Qty * (double)oi.Price / 100)) - oi.Qty * (double)oi.Product.Cost))
            }).ToList();

            // Create series for monthly data
            var revenueSeries = new GraphDataSeries
            {
                Id = "Monthly Revenue",
                Data = new List<GraphDataPoint>()
            };
            var costsSeries = new GraphDataSeries
            {
                Id = "Monthly Costs",
                Data = new List<GraphDataPoint>()
            };
            var profitsSeries = new GraphDataSeries
            {
                Id = "Monthly Profits",
                Data = new List<GraphDataPoint>()
            };

            // Fill the data for each month (January to December)
            for (int i = 1; i <= 12; i++)
            {
                var monthData = groupedData.FirstOrDefault(g => g.Month == i);
                var monthName = new DateTime(year, i, 1).ToString("MMMM"); // Get full month name

                revenueSeries.Data.Add(new GraphDataPoint
                {
                    X = monthName,
                    Y = monthData?.Revenue ?? 0 // If no data for the month, show 0
                });

                costsSeries.Data.Add(new GraphDataPoint
                {
                    X = monthName,
                    Y = monthData?.Cost ?? 0
                });

                profitsSeries.Data.Add(new GraphDataPoint
                {
                    X = monthName,
                    Y = monthData?.Profit ?? 0
                });
            }

            // Add the series to the final data list
            data.Add(revenueSeries);
            data.Add(costsSeries);
            data.Add(profitsSeries);

            return data;
        }
        public async Task<List<GraphDataSeries>> GetDailyRevenueProfitCostForWeeks()
        {
            var data = new List<GraphDataSeries>();

            // Fetch completed orders with related data
            var orders = await _context.Orders
                .Where(x => x.OrderStatus == "completed")
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            // Determine the start of the current week (e.g., Monday)
            var currentWeekStart = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);

            // Group orders by each day of the current week
            var groupedData = orders
                .Where(o => o.UpdatedAt.Date >= currentWeekStart && o.UpdatedAt.Date < currentWeekStart.AddDays(7))
                .GroupBy(o => o.UpdatedAt.Date)
                .Select(g => new
                {
                    Day = g.Key,
                    Revenue = g.Sum(o => o.OrderItems
                        .Sum(oi => (oi.Qty * (double)oi.Price - ((double)oi.Discount * oi.Qty * (double)oi.Price / 100)))),
                    Cost = g.Sum(o => o.OrderItems
                        .Sum(oi => (oi.Qty * (double)oi.Product.Cost))),
                    Profit = g.Sum(o => o.OrderItems
                        .Sum(oi => (oi.Qty * (double)oi.Price - ((double)oi.Discount * oi.Qty * (double)oi.Price / 100)) - oi.Qty * (double)oi.Product.Cost))
                }).ToList();

            // Create series for daily data
            var revenueSeries = new GraphDataSeries
            {
                Id = "Daily Revenue",
                Data = new List<GraphDataPoint>()
            };
            var costsSeries = new GraphDataSeries
            {
                Id = "Daily Costs",
                Data = new List<GraphDataPoint>()
            };
            var profitsSeries = new GraphDataSeries
            {
                Id = "Daily Profits",
                Data = new List<GraphDataPoint>()
            };

            // Fill the data for each day in the current week
            for (int i = 0; i < 7; i++)
            {
                var day = currentWeekStart.AddDays(i);
                var dayData = groupedData.FirstOrDefault(g => g.Day == day);

                revenueSeries.Data.Add(new GraphDataPoint
                {
                    X = day.ToString("dd/MM"), // e.g., "Monday"
                    Y = dayData?.Revenue ?? 0 // If no data for the day, show 0
                });

                costsSeries.Data.Add(new GraphDataPoint
                {
                    X = day.ToString("dd/MM"),
                    Y = dayData?.Cost ?? 0
                });

                profitsSeries.Data.Add(new GraphDataPoint
                {
                    X = day.ToString("dd/MM"),
                    Y = dayData?.Profit ?? 0
                });
            }

            // Add the series to the final data list
            data.Add(revenueSeries);
            data.Add(costsSeries);
            data.Add(profitsSeries);

            return data;
        }



        private string GetMonthName(int month)
        {
            return new DateTime(1, month, 1).ToString("MMMM");
        }
    }
}
