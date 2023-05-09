using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class GetUserIdCommandHandlerBuilder
{
    public GetUserIdCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public GetUserIdCommandHandlerStub Build()
    {
        return new GetUserIdCommandHandlerStub(UserService);
    }
}