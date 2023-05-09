using Users.Bll.Commands;
using Users.Bll.Models;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class GetUserIdCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new GetUserIdCommand(
            new Random(2908).Next(1, 100)
        );

        var builder = new GetUserIdCommandHandlerBuilder();

        builder.UserService.SetupQueryUserId(new User());

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyQueryUserIdWasCalledOnce(command.UserId);
        handler.UserService.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_ThrowArgumentNullException_WhenNotFoundUser()
    {
        var command = new GetUserIdCommand(
            new Random(2908).Next(1, 100)
        );

        var builder = new GetUserIdCommandHandlerBuilder();

        builder.UserService.SetupQueryUserId(null);

        var handler = builder.Build();


        await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(command, default));

        handler.UserService.VerifyQueryUserIdWasCalledOnce(command.UserId);
        handler.UserService.VerifyNoOtherCalls();
    }
}