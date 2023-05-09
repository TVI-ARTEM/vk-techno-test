using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class RemoveUserIdCommandHandlerBuilder
{
    public RemoveUserIdCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public RemoveUserIdCommandHandlerStub Build()
    {
        return new RemoveUserIdCommandHandlerStub(UserService);
    }
}