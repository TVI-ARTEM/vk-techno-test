using FluentValidation;
using Users.API.Requests;

namespace Users.API.Validation;

public class GetUserLoginRequestValidator : AbstractValidator<UserLoginRequest>
{
    public GetUserLoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
    }
}