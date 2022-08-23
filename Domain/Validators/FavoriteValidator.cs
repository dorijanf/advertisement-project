using FluentValidation;
using SharedModels.Dtos;

namespace Domain.Validators
{
    public class FavoriteValidator : AbstractValidator<FavoriteDto>
    {
        public FavoriteValidator()
        {
            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .WithMessage("User email is required.")
                .EmailAddress()
                .WithMessage("User email must be in correct format.");
        }
    }
}
