using Moq;
using Users.Bll.Commands;
using Users.Bll.Services.Interfaces;

namespace Users.UnitTests.Stubs;

public class AuthUserCommandHandlerStub : AuthUserCommandHandler
{
    public AuthUserCommandHandlerStub(Mock<IUserService> userService) : base(userService.Object)
    {
        UserService = userService;
    }

    public Mock<IUserService> UserService { get; }

    public void VerifyNoOtherCalls()
    {
        UserService.VerifyNoOtherCalls();
    }
}