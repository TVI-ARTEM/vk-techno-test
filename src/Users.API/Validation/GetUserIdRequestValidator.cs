using FluentValidation;
using Users.API.Requests;

namespace Users.API.Validation;

public class GetUserIdRequestValidator : AbstractValidator<UserIdRequest>
{
    public GetUserIdRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}