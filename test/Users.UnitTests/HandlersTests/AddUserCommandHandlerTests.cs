using Users.Bll.Commands;
using Users.Bll.Models;
using Users.Bll.Queries;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class AddUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new AddUserCommand(
            "Login",
            "Password",
            DateTime.Now,
            "Admin",
            "Description",
            "Description"
        );

        var request = new AddUserRequest(
            command.Login,
            command.Password,
            command.CreateDate,
            command.GroupCode == "Admin" ? UserGroupEnum.Admin : UserGroupEnum.User,
            command.GroupDescription,
            command.StateDescription
        );

        var builder = new AddUserCommandHandlerBuilder();

        builder.UserService.SetupQueryUserGroupEnum(new List<User>());
        builder.UserService.SetupQueryUserLogin(null);

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyQueryGroupEnumWasCalledOnce(UserGroupEnum.Admin);
        handler.UserService.VerifyQueryUserLoginWasCalledOnce(command.Login);
        handler.UserService.VerifyAddUserWasCalledOnce(request);
        handler.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ThrowArgumentException_WhenIncorrectGroupCode()
    {
        var command = new AddUserCommand(
            "Login",
            "Password",
            DateTime.Now,
            "INCORRECT",
            "Description",
            "Description"
        );

        var builder = new AddUserCommandHandlerBuilder();

        builder.UserService.SetupQueryUserGroupEnum(new List<User>());
        builder.UserService.SetupQueryUserLogin(null);

        var handler = builder.Build();

        await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(command, default));

        handler.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ThrowArgumentException_WhenHaveAdmin()
    {
        var command = new AddUserCommand(
            "Login",
            "Password",
            DateTime.Now,
            "Admin",
            "Description",
            "Description"
        );

        var builder = new AddUserCommandHandlerBuilder();

        builder.UserService.SetupQueryUserGroupEnum(new[]
            { new User { UserState = new UserState { Code = UserStateEnum.Active } } });
        builder.UserService.SetupQueryUserLogin(null);

        var handler = builder.Build();

        await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(command, default));

        handler.UserService.VerifyQueryGroupEnumWasCalledOnce(UserGroupEnum.Admin);
        handler.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ThrowArgumentException_WhenHaveSameLogin()
    {
        var command = new AddUserCommand(
            "Login",
            "Password",
            DateTime.Now,
            "Admin",
            "Description",
            "Description"
        );


        var builder = new AddUserCommandHandlerBuilder();

        builder.UserService.SetupQueryUserGroupEnum(new List<User>());
        builder.UserService.SetupQueryUserLogin(new User { Login = command.Login });

        var handler = builder.Build();

        await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(command, default));

        handler.UserService.VerifyQueryGroupEnumWasCalledOnce(UserGroupEnum.Admin);
        handler.UserService.VerifyQueryUserLoginWasCalledOnce(command.Login);
        handler.VerifyNoOtherCalls();
    }
}