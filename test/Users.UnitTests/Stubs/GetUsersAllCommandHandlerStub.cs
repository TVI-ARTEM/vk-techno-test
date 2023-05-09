using Moq;
using Users.Bll.Commands;
using Users.Bll.Services.Interfaces;

namespace Users.UnitTests.Stubs;

public class GetUsersAllCommandHandlerStub : GetUsersAllCommandHandler
{
    public GetUsersAllCommandHandlerStub(Mock<IUserService> userService) : base(userService.Object)
    {
        UserService = userService;
    }

    public Mock<IUserService> UserService { get; }

    public void VerifyNoOtherCalls()
    {
        UserService.VerifyNoOtherCalls();
    }
}