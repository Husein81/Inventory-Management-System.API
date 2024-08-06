using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Category>> CreateCategory(Category request)
        {
            await _context.Categories.AddAsync(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Category>.Success(request)
                : Response<Category>.Fail("Failed to create category");
        }

        public async Task<Response<Unit>> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            
            if(category is null)
            {
                return Response<Unit>.Fail($"Category with id :{id} not found");
            }

            _context.Categories.Remove(category);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Unit>.Success(Unit.Value)
                : Response<Unit>.Fail("Failed to delete category");
        }

        public async Task<Response<PagedList<Category>>> GetCategories(int page, int pageSize)
        {
            var categories = _context.Categories.AsQueryable();
            return Response<PagedList<Category>>.Success(
                await PagedList<Category>.ToPagedList(categories, page, pageSize));
        }

        public async Task<Response<Category>> GetCategory(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return Response<Category>.Fail($"Category with id :{id} not found");
            }

            return Response<Category>.Success(category);
        }

        public async Task<Response<Category>> UpdateCategory(Guid id, Category request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                
            if (category is null)
            {
                return Response<Category>.Fail($"Category with id :{id} not found");
            }

            category.Update(request.Name, request.Description,request.ImageUrl);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Category>.Success(category)
                : Response<Category>.Fail("Failed to update category");
        }
    }
}
