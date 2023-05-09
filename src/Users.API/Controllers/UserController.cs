using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.API.Auth;
using Users.API.Requests;
using Users.API.Responses;
using Users.Bll.Commands;

namespace Users.API.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [BasicAuthorization]
    public async Task<GetUserResponse> GetById([FromQuery] UserIdRequest idRequest, CancellationToken token)
    {
        var user = await _mediator.Send(new GetUserIdCommand(idRequest.Id), token);

        return new GetUserResponse(new UserInfo
        (
            user.Id,
            user.Login,
            user.CreatedDate,
            new UserGroupInfo
            (
                user.UserGroup.Id,
                user.UserGroup.Code.ToString(),
                user.UserGroup.Description
            ),
            new UserStateInfo
            (
                user.UserState.Id,
                user.UserState.Code.ToString(),
                user.UserState.Description
            )
        ));
    }


    [HttpGet]
    [BasicAuthorization]
    public async Task<GetUserResponse> GetByLogin([FromQuery] UserLoginRequest idRequest, CancellationToken token)
    {
        var user = await _mediator.Send(new GetUserLoginCommand(idRequest.Login), token);

        return new GetUserResponse(new UserInfo
        (
            user.Id,
            user.Login,
            user.CreatedDate,
            new UserGroupInfo
            (
                user.UserGroup.Id,
                user.UserGroup.Code.ToString(),
                user.UserGroup.Description
            ),
            new UserStateInfo
            (
                user.UserState.Id,
                user.UserState.Code.ToString(),
                user.UserState.Description
            )
        ));
    }

    [HttpGet]
    [BasicAuthorization]
    public async Task<GetUsersResponse> GetAll(CancellationToken token)
    {
        var usersInfo = new List<UserInfo>();
        var users = await _mediator.Send(new GetUsersAllCommand(), token);
        foreach (var user in users)
            usersInfo.Add(new UserInfo
                (
                    user.Id,
                    user.Login,
                    user.CreatedDate,
                    new UserGroupInfo
                    (
                        user.UserGroup.Id,
                        user.UserGroup.Code.ToString(),
                        user.UserGroup.Description
                    ),
                    new UserStateInfo
                    (
                        user.UserState.Id,
                        user.UserState.Code.ToString(),
                        user.UserState.Description
                    )
                )
            );

        return new GetUsersResponse(usersInfo);
    }

    [HttpGet]
    [BasicAuthorization]
    public async Task<GetUsersResponse> GetQuery([FromQuery] GetUsersQueryRequest request, CancellationToken token)
    {
        var usersInfo = new List<UserInfo>();
        var users = await _mediator.Send(new GetUsersQueryCommand(request.Take, request.Skip), token);
        foreach (var user in users)
            usersInfo.Add(new UserInfo
                (
                    user.Id,
                    user.Login,
                    user.CreatedDate,
                    new UserGroupInfo
                    (
                        user.UserGroup.Id,
                        user.UserGroup.Code.ToString(),
                        user.UserGroup.Description
                    ),
                    new UserStateInfo
                    (
                        user.UserState.Id,
                        user.UserState.Code.ToString(),
                        user.UserState.Description
                    )
                )
            );

        return new GetUsersResponse(usersInfo);
    }

    [HttpPost]
    public async Task<AddUserResponse> Add([FromBody] AddUserRequest request, CancellationToken token)
    {
        await _mediator.Send(new AddUserCommand(
            request.Login,
            request.Password,
            request.GroupCode,
            request.GroupDescription,
            request.StateDescription
        ), token);

        return new AddUserResponse();
    }

    [HttpDelete]
    [BasicAuthorization]
    public async Task<RemoveUserResponse> RemoveById([FromBody] UserIdRequest request, CancellationToken token)
    {
        await _mediator.Send(new RemoveUserIdCommand(request.Id), token);

        return new RemoveUserResponse();
    }

    [HttpDelete]
    [BasicAuthorization]
    public async Task<RemoveUserResponse> RemoveByLogin([FromBody] UserLoginRequest request, CancellationToken token)
    {
        await _mediator.Send(new RemoveUserLoginCommand(request.Login), token);

        return new RemoveUserResponse();
    }
}