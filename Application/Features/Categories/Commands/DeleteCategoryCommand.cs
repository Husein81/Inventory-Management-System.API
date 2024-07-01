using Application.Repository;
using Domain;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Response<Unit>>;
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<Unit>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<Unit>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.DeleteCategory(request.Id);

            return category;
        }
    }
}
