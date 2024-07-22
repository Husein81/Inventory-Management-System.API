using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Products.Queries
{
    public record GetProductsQuery() : IRequest<Response<List<Product>>>;
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<List<Product>>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            => await _productRepository.GetProducts();
    }
}
