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
            return AuthenticateResult.Fail("Authorization header is missed");

        var authorizationHeader = Request.Headers["Authorization"].ToString();

        if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            return AuthenticateResult.Fail("Incorrect Authorization header. Must be start from 'Basic'");

        var token = authorizationHeader.Replace("Basic", "", StringComparison.OrdinalIgnoreCase).Trim();

        var authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        var tokens = authBase64Decoded.Split(':');

        if (tokens.Length != 2) return AuthenticateResult.Fail("Invalid Authorization header token format");

        var userLogin = tokens[0];
        var userPassword = tokens[1];

        if (!await _mediator.Send(new AuthUserCommand(userLogin, userPassword)))
            return AuthenticateResult.Fail($"'{userLogin}' - incorrect password");

        var claims = new[] { new Claim(ClaimTypes.Name, userLogin) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);

        return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
    }
}