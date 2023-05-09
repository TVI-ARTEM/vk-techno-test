using FluentValidation;
using Users.API.Request;

namespace Users.API.Validation;

public class GetUserLoginRequestValidator : AbstractValidator<GetUserLoginRequest>
{
    public GetUserLoginRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
    }
}