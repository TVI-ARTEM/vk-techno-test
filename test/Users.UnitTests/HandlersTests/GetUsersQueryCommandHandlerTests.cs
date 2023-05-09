using Users.Bll.Commands;
using Users.Bll.Models;
using Users.Bll.Queries;
using Users.UnitTests.Builders;
using Users.UnitTests.Extensions;
using Xunit;

namespace Users.UnitTests.HandlersTests;

public class GetUsersQueryCommandHandlerTests
{
    [Fact]
    public async Task Handle_MakeAllCals()
    {
        var command = new GetUsersQueryCommand(1, 0);
        var request = new QueryUserRequest(command.Take, command.Skip);

        var builder = new GetUsersQueryCommandHandlerBuilder();

        builder.UserService.SetupQueryUserLogin(new User());

        var handler = builder.Build();

        await handler.Handle(command, default);

        handler.UserService.VerifyQueryUserRequestWasCalledOnce(request);
        handler.UserService.VerifyNoOtherCalls();
    }
}