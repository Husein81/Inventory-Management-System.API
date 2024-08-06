using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Response;


namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Product>> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? Response<Product>.Success(product) 
                : Response<Product>.Fail("Failed to create product");
        }

        public async Task<Response<Unit>> DeleteProduct(Guid id)
        {
            var product = await _context.Products.Include(p => p.Supplier)
                .Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (product is null) 
            {
                return Response<Unit>.Fail($"Product with id:{id} not found");
            }

            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Unit>.Success(Unit.Value) 
                : Response<Unit>.Fail("Failed to delete product");

        }

        public async Task<Response<Product>> GetProductById(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
            {
                return Response<Product>.Fail($"Product with id:{id} not found");
            }
            return Response<Product>.Success(product);

        }

        public async Task<Response<PagedList<Product>>> GetProducts(int page, int pageSize, string searchTerm )
        {
            var products = _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                products = products.Where( p => p.Name.ToLower().Contains(searchTerm));
            }

            return Response<PagedList<Product>>.Success(
                await PagedList<Product>.ToPagedList(products, page, pageSize)); 
        }

        public async Task<Response<Product>> UpdateProduct(Guid Id, Product request)
        {
            var product = await _context.Products.Include(p => p.Supplier)
                .Include(p => p.Category).FirstOrDefaultAsync(x => x.Id == Id);

            if (product is null)
            {
                return Response<Product>.Fail($"Product with id:{Id} not found");
            }

            product.Update(request); 

            var result = await _context.SaveChangesAsync() > 0;
            return result ? Response<Product>.Success(product) 
                : Response<Product>.Fail("Failed to update product");
        }
    }
}
