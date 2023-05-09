using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record GetUserIdCommand(long UserId) : IRequest<User>;

public class GetUserIdCommandHandler : IRequestHandler<GetUserIdCommand, User>
{
    private readonly IUserService _service;

    public GetUserIdCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<User> Handle(GetUserIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _service.QueryUser(request.UserId, cancellationToken);
        if (user == null) throw new ArgumentNullException(nameof(user), "User with current id does not exist");

        return user;
    }
}