using System.Diagnostics;
using MediatR;
using Users.Bll.Models;
using Users.Bll.Queries;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Commands;

public record AddUserCommand(
    string Login,
    string Password,
    string GroupCode,
    string GroupDescription,
    string StateDescription
) : IRequest<Unit>;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Unit>
{
    private readonly IUserService _service;

    public AddUserCommandHandler(IUserService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var groupCode = request.GroupCode switch
        {
            "Admin" => UserGroupEnum.Admin,
            "User" => UserGroupEnum.User,
            _ => throw new ArgumentException("Incorrect user's group code", nameof(AddUserCommand.GroupCode))
        };

        if (groupCode == UserGroupEnum.Admin)
        {
            var users = await _service.QueryUsers(groupCode, cancellationToken);
            if (users.Any(it => it.UserState.Code == UserStateEnum.Active))
            {
                throw new ArgumentException("Admin user already exists", nameof(AddUserCommand.GroupCode));
            }
        }

        var userSameLogin = await _service.QueryUser(request.Login, cancellationToken);
        if (userSameLogin != null)
        {
            throw new ArgumentException("User with current login already exists", nameof(AddUserCommand.Login));
        }

        var addRequest = new AddUserRequest
        (
            Login: request.Login,
            Password: request.Password,
            CreatedDate: DateTime.Now,
            GroupCode: groupCode,
            GroupDescription: request.GroupDescription,
            StateDescription: request.StateDescription
        );

        await _service.Add(addRequest, cancellationToken);

        return new Unit();
    }
}