using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Products.Queries
{
    public record GetProductsQuery(int Page , int PageSize, string SearchTerm)  : IRequest<Response<PagedList<Product>>>;
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<PagedList<Product>>>
    {
        private readonly IProductRepository _productRepository;
        
        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<PagedList<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProducts(request.Page, request.PageSize, request.SearchTerm);
        }
    }
}
