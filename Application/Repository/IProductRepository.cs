using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Repository
{
    public interface IProductRepository
    {
        Task<Response<List<Product>>> GetProducts();
        Task<Response<Product>> GetProductById(Guid id);
        Task<Response<Product>> CreateProduct(Product product);
        Task<Response<Product>> UpdateProduct(Guid Id, Product product);
        Task<Response<Unit>> DeleteProduct(Guid id);
    }
}
