using Application.Repository;
using Domain;
using MediatR;
using Shared.Response;

namespace Application.Features.Categories.Queries
{
    public record GetCategoriesQuery : IRequest<Response<List<Category>>>;
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<List<Category>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetCategories();

            return categories;
        }
    }
}
