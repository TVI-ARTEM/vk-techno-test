using Moq;
using Users.Bll.Repositories.Interfaces;
using Users.UnitTests.Stubs;

namespace Users.UnitTests.Builders;

public class UserServiceBuilder
{
    public UserServiceBuilder()
    {
        UserRepository = new Mock<IUserRepository>();
    }

    public Mock<IUserRepository> UserRepository { get; }

    public UserServiceStub Build()
    {
        return new UserServiceStub(UserRepository);
    }
}