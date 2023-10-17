using Application.Regexes;
using Contracts.Constants;
using Contracts.Requests;
using FluentValidation;

namespace Presentation.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(e => e.CurrentPassword).NotEmpty()
            .Matches(ValidationRegexes.Length8AtLeastOneCharAndDigitPasswordRegex())
            .WithMessage($"Current password: {ValidationErrorMessages.InvalidPasswordFormat}");
        RuleFor(e => e.NewPassword).NotEmpty()
            .Matches(ValidationRegexes.Length8AtLeastOneCharAndDigitPasswordRegex())
            .WithMessage($"New password: {ValidationErrorMessages.InvalidPasswordFormat}");
        RuleFor(e => e.CurrentPassword).NotEmpty()
            .Equal(e => e.ConfirmNewPassword)
            .WithMessage(ValidationErrorMessages.ConfirmPasswordShouldBeSameWithPassword);
    }
}