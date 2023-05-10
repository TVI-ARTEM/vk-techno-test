using Users.Bll.Commands;
using Users.Bll.Models;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class GetUsersAllCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new GetUsersAllCommand();

        var builder = new GetUsersAllCommandHandlerBuilder();

        builder.UserService.SetupQueryUserAll(new[] { new User() });

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyQueryAllWasCalledOnce();
        handler.VerifyNoOtherCalls();
    }
}