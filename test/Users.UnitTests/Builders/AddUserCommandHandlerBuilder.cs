using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class AddUserCommandHandlerBuilder
{
    public AddUserCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public AddUserCommandHandlerStub Build()
    {
        return new AddUserCommandHandlerStub(UserService);
    }
}