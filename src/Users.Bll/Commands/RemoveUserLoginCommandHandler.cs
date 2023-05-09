using MediatR;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record RemoveUserLoginCommand(string Login) : IRequest<Unit>;

public class RemoveUserLoginCommandHandler : IRequestHandler<RemoveUserLoginCommand, Unit>
{
    private readonly IUserService _service;

    public RemoveUserLoginCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(RemoveUserLoginCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveUser(request.Login, cancellationToken);
        return new Unit();
    }
}