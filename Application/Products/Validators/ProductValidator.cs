using Application.Products.Commands;
using Application.Repository;
using Domain;
using FluentValidation;

namespace Application.Products.Validators
{
    public class ProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public ProductValidator(IProductRepository productRepository)
        {

        }
    }
}
