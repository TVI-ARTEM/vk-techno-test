using Users.Bll.Commands;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class RemoveUserIdCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new RemoveUserIdCommand(
            new Random(2908).Next(1, 100)
        );

        var builder = new RemoveUserIdCommandHandlerBuilder();

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyRemoveUserWasCalledOnce(command.UserId);
        handler.VerifyNoOtherCalls();
    }
}