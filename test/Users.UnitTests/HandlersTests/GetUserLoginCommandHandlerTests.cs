using Users.Bll.Commands;
using Users.Bll.Models;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class GetUserLoginCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new GetUserLoginCommand(
            "SomeLogin"
        );

        var builder = new GetUserLoginCommandHandlerBuilder();

        builder.UserService.SetupQueryUserLogin(new User());

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyQueryUserLoginWasCalledOnce(command.Login);
        handler.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ThrowArgumentNullException_WhenNotFoundUser()
    {
        var command = new GetUserLoginCommand(
            "SomeLogin"
        );

        var builder = new GetUserLoginCommandHandlerBuilder();

        builder.UserService.SetupQueryUserLogin(null);

        var handler = builder.Build();

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(command, default));

        handler.UserService.VerifyQueryUserLoginWasCalledOnce(command.Login);
        handler.VerifyNoOtherCalls();
    }
}