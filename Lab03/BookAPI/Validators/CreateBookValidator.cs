using BookAPI.Features.Books;
using FluentValidation;

namespace BookAPI.Validators;

public class CreateBookValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookValidator()
    {
        RuleFor(b => b.Title).NotEmpty();
        RuleFor(b => b.Author).NotEmpty();
        RuleFor(b => b.Year)
            .InclusiveBetween(1500, DateTime.Now.Year)
            .WithMessage("Year must be between 1500 and current year.");
    }
}