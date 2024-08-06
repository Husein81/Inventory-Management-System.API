using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Repository
{
    public interface ICategoryRepository
    {
        Task<Response<PagedList<Category>>> GetCategories(int page, int pageSize);
        Task<Response<Category>> GetCategory(Guid id);
        Task<Response<Category>> CreateCategory(Category request);
        Task<Response<Category>> UpdateCategory(Guid Id,Category request);
        Task<Response<Unit>> DeleteCategory(Guid id);
    }
}
