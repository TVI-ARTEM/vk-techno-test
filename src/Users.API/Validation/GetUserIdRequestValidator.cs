using FluentValidation;
using Users.API.Request;

namespace Users.API.Validation;

public class GetUserIdRequestValidator : AbstractValidator<GetUserIdRequest>
{
    public GetUserIdRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}