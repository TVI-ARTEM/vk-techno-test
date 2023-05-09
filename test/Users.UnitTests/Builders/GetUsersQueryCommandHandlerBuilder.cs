using Moq;
using Users.Bll.Services.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class GetUsersQueryCommandHandlerBuilder
{
    public GetUsersQueryCommandHandlerBuilder()
    {
        UserService = new Mock<IUserService>();
    }

    public Mock<IUserService> UserService { get; }

    public GetUsersQueryCommandHandlerStub Build()
    {
        return new GetUsersQueryCommandHandlerStub(UserService);
    }
}