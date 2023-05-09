using MediatR;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record RemoveUserIdCommand(long UserId) : IRequest<Unit>;

public class RemoveUserIdCommandHandler : IRequestHandler<RemoveUserIdCommand, Unit>
{
    private readonly IUserService _service;

    public RemoveUserIdCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(RemoveUserIdCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveUser(request.UserId, cancellationToken);
        return new Unit();
    }
}