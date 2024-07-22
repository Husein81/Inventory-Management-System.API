using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Requests;
using Shared.Response;

namespace Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(Guid Id, CategoryRequest request) : IRequest<Response<Category>>;
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.request);

            return await _categoryRepository.UpdateCategory(request.Id, category);
        }
    }
}
