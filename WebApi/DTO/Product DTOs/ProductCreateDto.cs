using FluentValidation;

namespace WebApi.DTO.Product_DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Not empty").MaximumLength(30).WithMessage("Max character: 30");
            RuleFor(p => p.Price).NotEmpty().WithMessage("Not empty").GreaterThan(10).WithMessage("Price can not be lower than 10");
            RuleFor(p => p.IsActive).NotEmpty().WithMessage("Not empty");
            RuleFor(p => p).Custom((p, context) =>
            {
                if (p.Price<p.DiscountPrice)
                {
                    context.AddFailure("Price", "Discount price can not be higher than regular price");
                }
            });
        }
    }
}
