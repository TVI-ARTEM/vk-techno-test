using Moq;
using Users.Bll.Models;
using Users.Bll.Queries;
using Users.Bll.Repositories.Interfaces;

namespace Users.UnitTests.Extensions;

public static class UserRepositoryExtensions
{
    public static Mock<IUserRepository> SetupQueryId(
        this Mock<IUserRepository> repository,
        User? user)
    {
        repository.Setup(p =>
            p.Query(It.IsAny<long>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        return repository;
    }

    public static Mock<IUserRepository> SetupQueryLogin(
        this Mock<IUserRepository> repository,
        User? user)
    {
        repository.Setup(p =>
            p.Query(It.IsAny<string>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(user);

        return repository;
    }

    public static Mock<IUserRepository> SetupQueryGroupEnum(
        this Mock<IUserRepository> repository,
        IEnumerable<User> users)
    {
        repository.Setup(p =>
            p.Query(It.IsAny<UserGroupEnum>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(users);

        return repository;
    }

    public static Mock<IUserRepository> SetupQueryUserRequest(
        this Mock<IUserRepository> repository,
        IEnumerable<User> users)
    {
        repository.Setup(p =>
            p.Query(It.IsAny<QueryUserRequest>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(users);

        return repository;
    }

    public static Mock<IUserRepository> SetupQueryAll(
        this Mock<IUserRepository> repository,
        IEnumerable<User> users)
    {
        repository.Setup(p =>
            p.Query(It.IsAny<CancellationToken>())
        ).ReturnsAsync(users);

        return repository;
    }

    public static Mock<IUserRepository> VerifyQueryIdWasCalledOnce(
        this Mock<IUserRepository> repository,
        long userId)
    {
        repository.Verify(p =>
                p.Query(It.Is<long>(x => x == userId), It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }

    public static Mock<IUserRepository> VerifyQueryLoginWasCalledOnce(
        this Mock<IUserRepository> repository,
        string login)
    {
        repository.Verify(p =>
                p.Query(It.Is<string>(x => x == login), It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }

    public static Mock<IUserRepository> VerifyQueryGroupEnumWasCalledOnce(
        this Mock<IUserRepository> repository,
        UserGroupEnum userGroup)
    {
        repository.Verify(p =>
                p.Query(It.Is<UserGroupEnum>(x => x == userGroup),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }

    public static Mock<IUserRepository> VerifyQueryUserRequestWasCalledOnce(
        this Mock<IUserRepository> repository,
        QueryUserRequest request)
    {
        repository.Verify(p =>
                p.Query(It.Is<QueryUserRequest>(x => x == request),
                    It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }

    public static Mock<IUserRepository> VerifyQueryAllWasCalledOnce(
        this Mock<IUserRepository> repository)
    {
        repository.Verify(p =>
                p.Query(It.IsAny<CancellationToken>()), Times.Once
        );

        return repository;
    }

    public static Mock<IUserRepository> VerifyAddWasCalledOnce(
        this Mock<IUserRepository> repository,
        AddUserRequest request)
    {
        repository.Verify(p =>
                p.Add(It.Is<AddUserRequest>(x => x == request), It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }

    public static Mock<IUserRepository> VerifyRemoveWasCalledOnce(
        this Mock<IUserRepository> repository,
        long userId)
    {
        repository.Verify(p =>
                p.Remove(It.Is<long>(x => x == userId), It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }

    public static Mock<IUserRepository> VerifyRemoveWasCalledOnce(
        this Mock<IUserRepository> repository,
        string login)
    {
        repository.Verify(p =>
                p.Remove(It.Is<string>(x => x == login), It.IsAny<CancellationToken>()),
            Times.Once);

        return repository;
    }
}