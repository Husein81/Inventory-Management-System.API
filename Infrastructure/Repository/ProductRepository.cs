using Application.Repository;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;


namespace Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

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
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
            {
                return Response<Product>.Fail($"Product with id:{id} not found");
            }
            return Response<Product>.Success(product);

        }

        public async Task<Response<List<Product>>> GetProducts()
            => Response<List<Product>>.Success(await _context.Products.ToListAsync());

        public async Task<Response<Product>> UpdateProduct(Guid id, Product request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
            {
                return Response<Product>.Fail($"Product with id:{id} not found");
            }

            product.Update(request);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? Response<Product>.Success(product) 
                : Response<Product>.Fail("Failed to update product");
        }
    }
}
