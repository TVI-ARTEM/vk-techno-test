using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record GetUserLoginCommand(string Login) : IRequest<User>;

public class GetUserLoginCommandHandler : IRequestHandler<GetUserLoginCommand, User>
{
    private readonly IUserService _service;

    public GetUserLoginCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<User> Handle(GetUserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _service.QueryUser(request.Login, cancellationToken);
        if (user == null) throw new ArgumentNullException(nameof(user), "User with current id does not exist");

        return user;
    }
}