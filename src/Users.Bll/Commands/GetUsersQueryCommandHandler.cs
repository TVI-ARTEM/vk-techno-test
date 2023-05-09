using MediatR;
using Users.Bll.Models;
using Users.Bll.Queries;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record GetUsersQueryCommand(int Take, int Skip) : IRequest<IEnumerable<User>>;

public class GetUsersQueryCommandHandler : IRequestHandler<GetUsersQueryCommand, IEnumerable<User>>
{
    private readonly IUserService _service;

    public GetUsersQueryCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersQueryCommand request, CancellationToken cancellationToken)
    {
        return await _service.QueryUsers(
            new QueryUserRequest(request.Take, request.Skip),
            cancellationToken);
    }
}