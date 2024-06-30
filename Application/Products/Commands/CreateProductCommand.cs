using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;
using Shared.Response;


namespace Application.Products.Commands
{
    public record  CreateProductCommand(Product product) : IRequest<Response<Product>>;
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
            var product = _mapper.Map<Product>(request.product);
            return await _productRepository.CreateProduct(request.product);
        }
    }
}
