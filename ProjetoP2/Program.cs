using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Application.UseCases.Appointment;
using ProjetoP2.Clinic.Application.UseCases.Vet;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Clinic.Infrastructure.Repositories;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Interface.Middlewares;
using ProjetoP2.Register.Application.UseCases.OwnerUseCases;
using ProjetoP2.Register.Application.UseCases.PetUseCases;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IOwnerRegisterRepository, OwnerRegisterRepository>();
builder.Services.AddScoped<IPetRegisterRepository, PetRegisterRepository>();
builder.Services.AddScoped<IAppointmentRegisterRepository, AppointmentRegisterRepository>();

builder.Services.AddScoped<IVetClinicRepository, VetClinicRepository>();
builder.Services.AddScoped<IAppointmentClinicRepository, AppointmentClinicRepository>();

builder.Services.AddScoped<CreateOwnerRegisterUseCase>();
builder.Services.AddScoped<GetOwnerRegisterUseCase>();
builder.Services.AddScoped<DeleteOwnerRegisterUseCase>();

builder.Services.AddScoped<CreatePetRegisterUseCase>();
builder.Services.AddScoped<GetPetRegisterUseCase>();
builder.Services.AddScoped<DeletePetRegisterUseCase>();

builder.Services.AddScoped<CreateVetClinicUseCase>();
builder.Services.AddScoped<DeleteVetClinicUseCase>();
builder.Services.AddScoped<GetVetClinicUseCase>();

builder.Services.AddScoped<CreateAppointmentClinicUseCase>();
builder.Services.AddScoped<DeleteAppointmentClinicUseCase>();
builder.Services.AddScoped<GetAppointmentClinicUseCase>();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? "DefaultConnection";
builder.Services.AddDbContext<Context>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

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

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
