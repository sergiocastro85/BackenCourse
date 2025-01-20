
using FluentValidation;
using WebApplicationBackend.DTOs;

namespace WebApplicationBackend.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(x=> x.Name)
                .Length(2,5)
                .WithMessage("The name must be greater than two and less than five in lenght");

            
        }

    }
}
