using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class GetUsersAllCommandHandlerBuilder
{
    public GetUsersAllCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public GetUsersAllCommandHandlerStub Build()
    {
        return new GetUsersAllCommandHandlerStub(UserService);
    }
}