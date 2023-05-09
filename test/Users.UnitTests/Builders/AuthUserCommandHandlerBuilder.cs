using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class AuthUserCommandHandlerBuilder
{
    public AuthUserCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public AuthUserCommandHandlerStub Build()
    {
        return new AuthUserCommandHandlerStub(UserService);
    }
}