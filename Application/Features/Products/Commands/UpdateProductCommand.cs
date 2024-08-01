using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Requests;
using Shared.Response;

namespace Application.Features.Products.Commands
{
    public record UpdateProductCommand(Guid Id, ProductRequest Product) : IRequest<Response<Product>>;
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
            var product = _mapper.Map<Product>(request.Product);
            return await _productRepository.UpdateProduct(request.Id, product);
        }
    }
}
