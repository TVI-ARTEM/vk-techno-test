using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record AuthUserCommand(
    string Login,
    string Password
) : IRequest<bool>;

public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, bool>
{
    private readonly IUserService _service;

    public AuthUserCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _service.QueryUser(request.Login, cancellationToken);

        return user is { UserState.Code: UserStateEnum.Active } &&
               user.Password.ToLower() == request.Password;
    }
}