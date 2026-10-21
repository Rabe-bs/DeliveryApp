using FluentValidation;

namespace DeliveryApp.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.RestaurantId)
                .NotEmpty().WithMessage("Restaurent identity Requierd.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name Product Requierd .")
                .MaximumLength(100).WithMessage("Product Name Max lenght is 100 litter .");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product Price shuld be great then zero.");
        }
    }
} 