using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries
{
    public record class GetCategoryQuery(Guid Id) : IRequest<Response<Category>>;
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Response<Category>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategory(request.Id);

            return category;
        }
    }
}
