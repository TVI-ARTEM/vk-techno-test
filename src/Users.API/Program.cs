using FluentValidation.AspNetCore;
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
    .AddDalInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.MigrateUp();
app.Run();