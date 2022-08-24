using FluentValidation;
using SharedModels.Dtos;

namespace Domain.Validators
{
    /// <summary>
    /// Favorite validator, currently only has one property (the email) so only
    /// the email will get validated.
    /// </summary>
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
