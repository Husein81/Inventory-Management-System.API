using Domain;
using MediatR;
using Shared.Response;

namespace Application.Repository
{
    public interface ICategoryRepository
    {
        Task<Response<List<Category>>> GetCategories();
        Task<Response<Category>> GetCategory(Guid id);
        Task<Response<Category>> CreateCategory(Category request);
        Task<Response<Category>> UpdateCategory(Guid Id,Category request);
        Task<Response<Unit>> DeleteCategory(Guid id);
    }
}
