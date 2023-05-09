using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.API.Request;
using Users.API.Response;
using Users.Bll.Commands;

namespace Users.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class UserController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("[action]")]
    public async Task<GetUserResponse> GetById([FromQuery] GetUserIdRequest idRequest, CancellationToken token)
    {
        var user = await _mediator.Send(new GetUserIdCommand(UserId: idRequest.Id), token);

        return new GetUserResponse(new UserInfo
        (
            Id: user.Id,
            Login: user.Login,
            CreatedDate: user.CreatedDate,
            UserGroupInfo: new UserGroupInfo
            (
                Id: user.UserGroup.Id,
                Code: user.UserGroup.Code.ToString(),
                Description: user.UserGroup.Description
            ),
            UserStateInfo: new UserStateInfo
            (
                Id: user.UserState.Id,
                Code: user.UserState.Code.ToString(),
                Description: user.UserState.Description
            )
        ));
    }


    [HttpGet("[action]")]
    public async Task<GetUserResponse> GetByLogin([FromQuery] GetUserLoginRequest idRequest, CancellationToken token)
    {
        var user = await _mediator.Send(new GetUserLoginCommand(Login: idRequest.Login), token);

        return new GetUserResponse(new UserInfo
        (
            Id: user.Id,
            Login: user.Login,
            CreatedDate: user.CreatedDate,
            UserGroupInfo: new UserGroupInfo
            (
                Id: user.UserGroup.Id,
                Code: user.UserGroup.Code.ToString(),
                Description: user.UserGroup.Description
            ),
            UserStateInfo: new UserStateInfo
            (
                Id: user.UserState.Id,
                Code: user.UserState.Code.ToString(),
                Description: user.UserState.Description
            )
        ));
    }

    [HttpGet("[action]")]
    public async Task<GetUsersResponse> GetAll(CancellationToken token)
    {
        var usersInfo = new List<UserInfo>();
        var users = await _mediator.Send(new GetUsersAllCommand(), token);
        foreach (var user in users)
        {
            usersInfo.Add(new UserInfo
                (
                    Id: user.Id,
                    Login: user.Login,
                    CreatedDate: user.CreatedDate,
                    UserGroupInfo: new UserGroupInfo
                    (
                        Id: user.UserGroup.Id,
                        Code: user.UserGroup.Code.ToString(),
                        Description: user.UserGroup.Description
                    ),
                    UserStateInfo: new UserStateInfo
                    (
                        Id: user.UserState.Id,
                        Code: user.UserState.Code.ToString(),
                        Description: user.UserState.Description
                    )
                )
            );
        }

        return new GetUsersResponse(usersInfo);
    }

    [HttpGet("[action]")]
    public async Task<GetUsersResponse> GetQuery([FromQuery] GetUsersQueryRequest request, CancellationToken token)
    {
        var usersInfo = new List<UserInfo>();
        var users = await _mediator.Send(new GetUsersQueryCommand(request.Take, request.Skip), token);
        foreach (var user in users)
        {
            usersInfo.Add(new UserInfo
                (
                    Id: user.Id,
                    Login: user.Login,
                    CreatedDate: user.CreatedDate,
                    UserGroupInfo: new UserGroupInfo
                    (
                        Id: user.UserGroup.Id,
                        Code: user.UserGroup.Code.ToString(),
                        Description: user.UserGroup.Description
                    ),
                    UserStateInfo: new UserStateInfo
                    (
                        Id: user.UserState.Id,
                        Code: user.UserState.Code.ToString(),
                        Description: user.UserState.Description
                    )
                )
            );
        }

        return new GetUsersResponse(usersInfo);
    }

    [HttpPost("[action]")]
    public async Task<AddUserResponse> AddUser([FromBody] AddUserRequest request, CancellationToken token)
    {
        await _mediator.Send(new AddUserCommand(
            Login: request.Login,
            Password: request.Password,
            GroupCode: request.GroupCode,
            GroupDescription: request.GroupDescription,
            StateDescription: request.StateDescription
        ), token);

        return new AddUserResponse();
    }
}