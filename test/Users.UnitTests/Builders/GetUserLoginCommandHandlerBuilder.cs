using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class GetUserLoginCommandHandlerBuilder
{
    public GetUserLoginCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public GetUserLoginCommandHandlerStub Build()
    {
        return new GetUserLoginCommandHandlerStub(UserService);
    }
}