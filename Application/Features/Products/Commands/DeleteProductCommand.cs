using Application.Repository;
using Domain;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Response<Unit>>;
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Unit>>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            => await _productRepository.DeleteProduct(request.Id);
    }
}
