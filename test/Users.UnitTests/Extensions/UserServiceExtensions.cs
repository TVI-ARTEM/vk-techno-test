using Moq;
using Users.Bll.Models;
using Users.Bll.Queries;
using Users.Bll.Services.Interfaces;

namespace Users.UnitTests.Extensions;

public static class UserServiceExtensions
{
    public static Mock<IUserService> SetupQueryUserId(
        this Mock<IUserService> service,
        User? user)
    {
        service.Setup(p =>
            p.QueryUser(It.IsAny<long>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        return service;
    }

    public static Mock<IUserService> SetupQueryUserLogin(
        this Mock<IUserService> service,
        User? user)
    {
        service.Setup(p =>
            p.QueryUser(It.IsAny<string>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        return service;
    }

    public static Mock<IUserService> SetupQueryUserGroupEnum(
        this Mock<IUserService> service,
        IEnumerable<User> users)
    {
        service.Setup(p =>
            p.QueryUsers(It.IsAny<UserGroupEnum>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(users);

        return service;
    }

    public static Mock<IUserService> SetupQueryUserRequest(
        this Mock<IUserService> service,
        IEnumerable<User> users)
    {
        service.Setup(p =>
            p.QueryUsers(It.IsAny<QueryUserRequest>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(users);

        return service;
    }

    public static Mock<IUserService> SetupQueryUserAll(
        this Mock<IUserService> service,
        IEnumerable<User> users)
    {
        service.Setup(p =>
            p.QueryUsers(It.IsAny<CancellationToken>())
        ).ReturnsAsync(users);

        return service;
    }

    public static Mock<IUserService> VerifyQueryUserIdWasCalledOnce(
        this Mock<IUserService> service,
        long userId)
    {
        service.Verify(p =>
                p.QueryUser(It.Is<long>(x => x == userId), It.IsAny<CancellationToken>()),
            Times.Once);

        return service;
    }

    public static Mock<IUserService> VerifyQueryUserLoginWasCalledOnce(
        this Mock<IUserService> service,
        string login)
    {
        service.Verify(p =>
                p.QueryUser(It.Is<string>(x => x == login), It.IsAny<CancellationToken>()),
            Times.Once);

        return service;
    }

    public static Mock<IUserService> VerifyQueryGroupEnumWasCalledOnce(
        this Mock<IUserService> service,
        UserGroupEnum userGroup)
    {
        service.Verify(p =>
                p.QueryUsers(It.Is<UserGroupEnum>(x => x == userGroup),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        return service;
    }

    public static Mock<IUserService> VerifyQueryUserRequestWasCalledOnce(
        this Mock<IUserService> service,
        QueryUserRequest request)
    {
        service.Verify(p =>
                p.QueryUsers(It.Is<QueryUserRequest>(x => x == request),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        return service;
    }

    public static Mock<IUserService> VerifyQueryAllWasCalledOnce(
        this Mock<IUserService> service)
    {
        service.Verify(p =>
                p.QueryUsers(It.IsAny<CancellationToken>()), Times.Once
        );

        return service;
    }

    public static Mock<IUserService> VerifyAddUserWasCalledOnce(
        this Mock<IUserService> service,
        AddUserRequest request)
    {
        service.Verify(p =>
                p.AddUser(It.Is<AddUserRequest>(x => x == request), It.IsAny<CancellationToken>()),
            Times.Once);

        return service;
    }

    public static Mock<IUserService> VerifyRemoveUserWasCalledOnce(
        this Mock<IUserService> service,
        long userId)
    {
        service.Verify(p =>
                p.RemoveUser(It.Is<long>(x => x == userId), It.IsAny<CancellationToken>()),
            Times.Once);

        return service;
    }

    public static Mock<IUserService> VerifyRemoveUserWasCalledOnce(
        this Mock<IUserService> service,
        string login)
    {
        service.Verify(p =>
                p.RemoveUser(It.Is<string>(x => x == login), It.IsAny<CancellationToken>()),
            Times.Once);

        return service;
    }
}