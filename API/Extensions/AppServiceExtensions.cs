using Application.Core;
using Application.Features.Products.Queries;
using Application.Products.Queries;
using Application.Repository;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetProductsQuery))));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            return services;
        }
    }
}
