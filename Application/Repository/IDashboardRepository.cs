using Application.Requests.DashboardDto;

namespace Application.Repository
{
    public interface IDashboardRepository
    { 
        Task<List<ProductDto>> GetLowStockItemsAsync();
        Task<List<ProductDto>> GetTopSellingProductsAsync();
        Task<List<GraphDataSeries>> GetRevenueProfitCost();
        Task<List<GraphDataSeries>> GetDailyRevenueProfitCostForWeeks();
    }
}
