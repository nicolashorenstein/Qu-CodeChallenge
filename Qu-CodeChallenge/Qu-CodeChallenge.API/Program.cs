using FluentValidation.AspNetCore;
using MediatR;
using Qu_CodeChallenge.CORE.Business;
using Qu_CodeChallenge.CORE.Interfaces.Matrix;
using Qu_CodeChallenge.CORE.Services.Matrix;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddControllers()
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<LoadMatrix>());


builder.Services.AddMediatR(typeof(LoadMatrix.Handler).Assembly);

builder.Services.AddScoped<IMatrixService, MatrixService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();
app.Run();