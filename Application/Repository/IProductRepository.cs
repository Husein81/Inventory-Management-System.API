using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Repository
{
    public interface IProductRepository
    {
        Task<Response<PagedList<Product>>> GetProducts(int pageNumber = 1, int pageSize = 10);
        Task<Response<Product>> GetProductById(Guid id);
        Task<Response<Product>> CreateProduct(Product product);
        Task<Response<Product>> UpdateProduct(Guid Id, Product product);
        Task<Response<Unit>> DeleteProduct(Guid id);
    }
}
