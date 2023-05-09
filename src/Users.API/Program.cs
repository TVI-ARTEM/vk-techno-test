using System.Net;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.SwaggerGen;
using Users.API.ActionFilters;
using Users.API.Auth;
using Users.Bll.Extensions;
using Users.Dal.Contexts;
using Users.Dal.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddControllers().Services
    .AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
        BasicAuthenticationDefaults.AuthenticationScheme, null).Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(ConfigureSwagger)
    .AddFluentValidation(conf =>
    {
        conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
        conf.AutomaticValidationEnabled = true;
    })
    .AddBllInfrastructure()
    .AddDalInfrastructure(builder.Configuration)
    .AddMvc(ConfigureMvc);


var app = builder.Build();

app.MigrateUp();

ReloadSqlTypes(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();


void ReloadSqlTypes(IServiceProvider appServices)
{
    using var scope = appServices.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<UserContext>();

    using var conn = (NpgsqlConnection)context.Database.GetDbConnection();
    conn.Open();
    conn.ReloadTypes();
}

void ConfigureMvc(MvcOptions x)
{
    x.Filters.Add(new ExceptionFilterAttribute());
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.BadRequest));
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.Forbidden));
    x.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
}

void ConfigureSwagger(SwaggerGenOptions o)
{
    o.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Basic Auth",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "basic",
        Type = SecuritySchemeType.Http
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
            },
            new List<string>()
        }
    });

    o.CustomSchemaIds(x => x.FullName);
}