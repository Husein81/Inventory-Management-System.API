using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;
using Shared.Response;

namespace Application.Products.Commands
{
    public record UpdateProductCommand(Guid Id, Product product) : IRequest<Response<Product>>;
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Response<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.product);
            return await _productRepository.UpdateProduct(request.Id, request.product);
        }
    }
}
