using FluentValidation;
using Users.API.Requests;

namespace Users.API.Validations;

public class GetUserLoginRequestValidator : AbstractValidator<UserLoginRequest>
{
    public GetUserLoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
    }
}