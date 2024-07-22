using Application.Features.Products.Commands;
using Application.Repository;
using Domain;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public class ProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public ProductValidator(IProductRepository productRepository)
        {


        }
    }
}
