using Microsoft.EntityFrameworkCore;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Register.Application.UseCases.OwnerUseCases;
using ProjetoP2.Register.Application.UseCases.PetUseCases;
using ProjetoP2.Register.Domain.IRepositories;

using ProjetoP2.Register.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();

builder.Services.AddScoped<CreateOwnerUseCase>();
builder.Services.AddScoped<GetOwnerUseCase>();
builder.Services.AddScoped<DeleteOwnerUseCase>();

builder.Services.AddScoped<CreatePetUseCase>();
builder.Services.AddScoped<GetPetUseCase>();
builder.Services.AddScoped<DeletePetUseCase>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
