using Application.Requests.DashboardDto;

namespace Application.Repository
{
    public interface IDashboardRepository
    {
        Task<double> GetTotalInventoryValueAsync();
        Task<List<ProductDto>> GetLowStockItemsAsync();
        Task<List<ProductDto>> GetTopSellingProductsAsync();
        Task<List<SalesTrendDto>> GetMonthlySalesTrendsAsync();
        Task<List<GraphDataSeries>> GetRevenueProfitCost();
        Task<List<GraphDataSeries>> GetDailyRevenueProfitCostForWeeks();
    }
}
