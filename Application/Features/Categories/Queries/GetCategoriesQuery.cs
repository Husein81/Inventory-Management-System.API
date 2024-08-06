using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Features.Categories.Queries
{
    public record GetCategoriesQuery(int page, int pageSize) : IRequest<Response<PagedList<Category>>>;
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<PagedList<Category>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<PagedList<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetCategories(request.page, request.pageSize);

            return categories;
        }
    }
}
