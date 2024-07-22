using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Requests;
using Shared.Response;


namespace Application.Features.Products.Commands
{
    public record CreateProductCommand(ProductRequest Product) : IRequest<Response<Product>>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Product>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Response<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.Product);
            product.CalculatePrice();

            return await _productRepository.CreateProduct(product);
        }
    }
}
