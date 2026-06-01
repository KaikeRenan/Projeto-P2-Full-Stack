using Microsoft.EntityFrameworkCore;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Register.Application.UseCases.OwnerUseCases;
using ProjetoP2.Register.Application.UseCases.PetUseCases;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IOwnerRegisterRepository, OwnerRegisterRepository>();
builder.Services.AddScoped<IPetRegisterRepository, PetRegisterRepository>();

builder.Services.AddScoped<CreateOwnerRegisterUseCase>();
builder.Services.AddScoped<GetOwnerRegisterUseCase>();
builder.Services.AddScoped<DeleteOwnerRegisterUseCase>();

builder.Services.AddScoped<CreatePetRegisterUseCase>();
builder.Services.AddScoped<GetPetRegisterUseCase>();
builder.Services.AddScoped<DeletePetRegisterUseCase>();

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
