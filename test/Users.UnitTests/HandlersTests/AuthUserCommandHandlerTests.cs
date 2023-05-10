using Users.Bll.Commands;
using Users.Bll.Models;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class AuthUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new AuthUserCommand(
            "Login",
            "Password"
        );

        var builder = new AuthUserCommandHandlerBuilder();

        builder.UserService.SetupQueryUserLogin(new User { Login = command.Login, Password = command.Password });

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyQueryUserLoginWasCalledOnce(command.Login);
        handler.VerifyNoOtherCalls();
    }
}