
using Merlebleu.Foundation.Exceptions.Handler;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
