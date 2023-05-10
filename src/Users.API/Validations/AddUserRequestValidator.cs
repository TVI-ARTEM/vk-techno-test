using FluentValidation;
using Users.API.Requests;

namespace Users.API.Validations;

public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(x => x.Login).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.GroupCode).NotEmpty();
        RuleFor(x => x.GroupDescription).NotEmpty();
        RuleFor(x => x.StateDescription).NotEmpty();
    }
}