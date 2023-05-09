using FluentValidation;
using Users.API.Request;

namespace Users.API.Validation;

public class GetUsersQueryRequestValidator : AbstractValidator<GetUsersQueryRequest>
{
    public GetUsersQueryRequestValidator()
    {
        RuleFor(x => x.Take).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
    }
}