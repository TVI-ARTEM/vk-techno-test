using Moq;
using Users.Bll.Repositories.Interfaces;
using Users.Bll.Services;

namespace Users.UnitTests.Stubs;

public class UserServiceStub : UserService
{
    public UserServiceStub(Mock<IUserRepository> userRepository) : base(userRepository.Object)
    {
        UserRepository = userRepository;
    }

    public Mock<IUserRepository> UserRepository { get; }

    public void VerifyNoOtherCalls()
    {
        UserRepository.VerifyNoOtherCalls();
    }
}