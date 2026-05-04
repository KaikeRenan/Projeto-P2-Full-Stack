using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Infrastructure.Data;
using ProjetoP2.Register.Application.UseCases.OwnerUseCases;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Infrastructure.Data;
using ProjetoP2.Register.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

builder.Services.AddScoped<CreateOwnerUseCase>();
builder.Services.AddScoped<GetOwnerUseCase>();
builder.Services.AddScoped<DeleteOwnerUseCase>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RegisterDBContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<ClinicDBContext>(options =>
    options.UseNpgsql(connectionString));

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
