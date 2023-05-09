using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class RemoveUserLoginCommandHandlerBuilder
{
    public RemoveUserLoginCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public RemoveUserLoginCommandHandlerStub Build()
    {
        return new RemoveUserLoginCommandHandlerStub(UserService);
    }
}