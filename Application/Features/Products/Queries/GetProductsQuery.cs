using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Products.Queries
{
    public record GetProductsQuery(int PageNumber , int PageSize)  : IRequest<Response<PagedList<Product>>>;
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<PagedList<Product>>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<PagedList<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            => await _productRepository.GetProducts(request.PageNumber, request.PageSize);
    }
}
