using Application.Repository;
using Domain;
using MediatR;
using Shared.Response;

namespace Application.Features.Products.Queries
{
    public record GetProductQuery(Guid Id) : IRequest<Response<Product>>;
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Response<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
            => await _productRepository.GetProductById(request.Id);
    }
}
