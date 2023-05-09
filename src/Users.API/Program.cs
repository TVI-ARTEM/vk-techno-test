using System.Net;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Users.API.ActionFilters;
using Users.Bll.Extensions;
using Users.Bll.Models;
using Users.Dal.Contexts;
using Users.Dal.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddControllers().Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(o => { o.CustomSchemaIds(x => x.FullName); })
    .AddFluentValidation(conf =>
    {
        conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
        conf.AutomaticValidationEnabled = true;
    })
    .AddBllInfrastructure(builder.Configuration)
    .AddDalInfrastructure(builder.Configuration)
    .AddMvc(ConfigureMvc);


services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = true;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();

app.MigrateUp();
app.Run();

void ConfigureMvc(MvcOptions x)
{
    x.Filters.Add(new ExceptionFilterAttribute());
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.BadRequest));
    x.Filters.Add(new ResponseTypeAttribute((int)HttpStatusCode.Forbidden));
    x.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
}