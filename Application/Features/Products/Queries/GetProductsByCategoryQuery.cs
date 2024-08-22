using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries
{
    public record GetProductsByCategoryQuery(Guid CategoryId, int Page, int PageSize) : IRequest<Response<PagedList<Product>>>;
    
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, Response<PagedList<Product>>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsByCategoryQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<PagedList<Product>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductsByCategory(request.CategoryId, request.Page, request.PageSize);
        }
    }
}
