using Users.Bll.Models;
using Users.Bll.Queries;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.ServicesTests;

public class UserServiceTests
{
    [Fact]
    public async Task QueryUserId_Success()
    {
        var userId = new Random(2908).Next(1, 100);
        var user = new User();

        var serviceBuilder = new UserServiceBuilder();
        serviceBuilder.UserRepository.SetupQueryId(user);
        var service = serviceBuilder.Build();

        var result = await service.QueryUser(userId, default);

        Assert.NotNull(result);
        Assert.Equal(result, user);
        service.UserRepository.VerifyQueryIdWasCalledOnce(userId);
        service.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task QueryUserLogin_Success()
    {
        const string userLogin = "SomeLogin";
        var user = new User();

        var serviceBuilder = new UserServiceBuilder();
        serviceBuilder.UserRepository.SetupQueryLogin(user);
        var service = serviceBuilder.Build();

        var result = await service.QueryUser(userLogin, default);

        Assert.NotNull(result);
        Assert.Equal(result, user);
        service.UserRepository.VerifyQueryLoginWasCalledOnce(userLogin);
        service.VerifyNoOtherCalls();
    }


    [Theory]
    [InlineData(UserGroupEnum.Admin)]
    [InlineData(UserGroupEnum.User)]
    public async Task QueryUserGroup_Success(UserGroupEnum userGroup)
    {
        var user = new User();

        var serviceBuilder = new UserServiceBuilder();
        serviceBuilder.UserRepository.SetupQueryGroupEnum(new[] { user });
        var service = serviceBuilder.Build();

        var result = await service.QueryUsers(userGroup, default);

        var collection = result.ToList();
        Assert.NotEmpty(collection);
        Assert.Contains(collection, it => it == user);
        service.UserRepository.VerifyQueryGroupEnumWasCalledOnce(userGroup);
        service.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task QueryUserRequest_Success()
    {
        var request = new QueryUserRequest(1, 0);
        var user = new User();

        var serviceBuilder = new UserServiceBuilder();
        serviceBuilder.UserRepository.SetupQueryUserRequest(new[] { user });
        var service = serviceBuilder.Build();

        var result = await service.QueryUsers(request, default);

        var collection = result.ToList();
        Assert.NotEmpty(collection);
        Assert.Contains(collection, it => it == user);
        service.UserRepository.VerifyQueryUserRequestWasCalledOnce(request);
        service.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task QueryUserAll_Success()
    {
        var user = new User();

        var serviceBuilder = new UserServiceBuilder();
        serviceBuilder.UserRepository.SetupQueryAll(new[] { user });
        var service = serviceBuilder.Build();

        var result = await service.QueryUsers(default);

        var collection = result.ToList();
        Assert.NotEmpty(collection);
        Assert.Contains(collection, it => it == user);
        service.UserRepository.VerifyQueryAllWasCalledOnce();
        service.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task AddUser_Success()
    {
        var request = new AddUserRequest(
            "Login",
            "Password",
            DateTime.Now,
            UserGroupEnum.Admin,
            "Description",
            "Description"
        );
        var serviceBuilder = new UserServiceBuilder();
        var service = serviceBuilder.Build();

        await service.AddUser(request, default);

        service.UserRepository.VerifyAddWasCalledOnce(request);
        service.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task RemoveUserId_Success()
    {
        var userId = new Random(2908).Next(1, 100);
        var serviceBuilder = new UserServiceBuilder();
        var service = serviceBuilder.Build();

        await service.RemoveUser(userId, default);

        service.UserRepository.VerifyRemoveWasCalledOnce(userId);
        service.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task RemoveUserLogin_Success()
    {
        const string userLogin = "SomeLogin";
        var serviceBuilder = new UserServiceBuilder();
        var service = serviceBuilder.Build();

        await service.RemoveUser(userLogin, default);

        service.UserRepository.VerifyRemoveWasCalledOnce(userLogin);
        service.VerifyNoOtherCalls();
    }
}