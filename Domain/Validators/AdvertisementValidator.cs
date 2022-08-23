using FluentValidation;
using SharedModels.Dtos;

namespace Domain.Validators
{
    /// <summary>
    /// AdvertisementValidator validates the incoming advertisement data
    /// transfer object. Current implementation is simple, all properties
    /// are required and the email must be in the correct format.
    /// </summary>
    public class AdvertisementValidator : AbstractValidator<AdvertisementDto>
    {
        public AdvertisementValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.");

            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .WithMessage("User email is required.")
                .EmailAddress()
                .WithMessage("User email must be in correct format.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Further information is required.");
        }
    }
}
