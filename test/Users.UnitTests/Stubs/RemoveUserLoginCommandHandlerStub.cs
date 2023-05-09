using Moq;
using Users.Bll.Commands;
using Users.Bll.Services.Interfaces;

namespace Users.UnitTests.Stubs;

public class RemoveUserLoginCommandHandlerStub : RemoveUserLoginCommandHandler
{
    public RemoveUserLoginCommandHandlerStub(Mock<IUserService> userService) : base(userService.Object)
    {
        UserService = userService;
    }

    public Mock<IUserService> UserService { get; }

    public void VerifyNoOtherCalls()
    {
        UserService.VerifyNoOtherCalls();
    }
}