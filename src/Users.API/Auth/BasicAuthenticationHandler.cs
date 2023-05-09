using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Users.Bll.Commands;

namespace Users.API.Auth;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IMediator _mediator;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IMediator mediator) : base(options, logger, encoder, clock)
    {
        _mediator = mediator;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization header");

        var authorizationHeader = Request.Headers["Authorization"].ToString();

        if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.Fail("Authorization header does not start with 'Basic'");

        var authBase64Decoded =
            Encoding.UTF8.GetString(
                Convert.FromBase64String(authorizationHeader.Replace("Basic ", "",
                    StringComparison.OrdinalIgnoreCase)));
        var authSplit = authBase64Decoded.Split(new[] { ':' }, 2);

        if (authSplit.Length != 2) return AuthenticateResult.Fail("Invalid Authorization header format");

        var clientLogin = authSplit[0];
        var clientPassword = authSplit[1];

        if (!await _mediator.Send(new AuthUserCommand(clientLogin, clientPassword)))
            return AuthenticateResult.Fail($"The password is incorrect for the client '{clientLogin}'");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, clientLogin)
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
    }
}