using FluentValidation;
using Users.API.Request;

namespace Users.API.Validation;

public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.GroupCode).NotEmpty();
    }
}