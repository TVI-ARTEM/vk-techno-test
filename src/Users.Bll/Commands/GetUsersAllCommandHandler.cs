using MediatR;
using Users.Bll.Models;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record GetUsersAllCommand : IRequest<IEnumerable<User>>;

public class GetUsersAllCommandHandler : IRequestHandler<GetUsersAllCommand, IEnumerable<User>>
{
    private readonly IUserService _service;

    public GetUsersAllCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersAllCommand request, CancellationToken cancellationToken)
    {
        return await _service.QueryUsers(cancellationToken);
    }
}