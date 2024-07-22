using Application.Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Requests;
using Shared.Response;


namespace Application.Features.Categories.Commands
{
    public record CreateCategoryCommand(CategoryRequest CategoryRequest) : IRequest<Response<Category>>;
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category =  _mapper.Map<Category>(request.CategoryRequest);

            return await _categoryRepository.CreateCategory(category);
        }
    }
}
