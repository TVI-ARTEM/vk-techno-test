using Users.Bll.Commands;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class RemoveUserLoginCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new RemoveUserLoginCommand(
            "SomeLogin"
        );

        var builder = new RemoveUserLoginCommandHandlerBuilder();

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyRemoveUserWasCalledOnce(command.Login);
        handler.VerifyNoOtherCalls();
    }
}