using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Repository
{
    public interface IProductRepository
    {
        Task<Response<PagedList<Product>>> GetProducts(int page, int pageSize , string searchTerm);
        Task<Response<Product>> GetProductById(Guid id);
        Task<Response<Product>> CreateProduct(Product product);
        Task<Response<Product>> UpdateProduct(Guid Id, Product product);
        Task<Response<Unit>> DeleteProduct(Guid id);
        Task<Response<PagedList<Product>>> GetProductsByCategory(Guid categoryId, int page, int pageSize);
    }
}
