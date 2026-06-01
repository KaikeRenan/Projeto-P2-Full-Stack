using Microsoft.EntityFrameworkCore;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Register.Application.UseCases.OwnerUseCases;
using ProjetoP2.Register.Application.UseCases.PetUseCases;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();

builder.Services.AddScoped<CreateOwnerUseCase>();
builder.Services.AddScoped<GetOwnerUseCase>();
builder.Services.AddScoped<DeleteOwnerUseCase>();

builder.Services.AddScoped<CreatePetUseCase>();
builder.Services.AddScoped<GetPetUseCase>();
builder.Services.AddScoped<DeletePetUseCase>();

builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
