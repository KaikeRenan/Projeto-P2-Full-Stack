using Microsoft.EntityFrameworkCore;
using ProjetoP2.Clinic.Application.UseCases.Appointment;
using ProjetoP2.Clinic.Application.UseCases.Vet;
using ProjetoP2.Clinic.Domain.IRepositories;
using ProjetoP2.Clinic.Infrastructure.Repositories;
using ProjetoP2.Infrastructure.Data.Context;
using ProjetoP2.Interface.Middlewares;
using ProjetoP2.Register.Application.UseCases.AppointmentRegisterUseCases;
using ProjetoP2.Register.Application.UseCases.OwnerUseCases;
using ProjetoP2.Register.Application.UseCases.PetUseCases;
using ProjetoP2.Register.Domain.IRepositories;
using ProjetoP2.Register.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("frontend", p => p.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers();

builder.Services.AddScoped<IOwnerRegisterRepository, OwnerRegisterRepository>();
builder.Services.AddScoped<IPetRegisterRepository, PetRegisterRepository>();
builder.Services.AddScoped<IAppointmentRegisterRepository, AppointmentRegisterRepository>();

builder.Services.AddScoped<IVetClinicRepository, VetClinicRepository>();
builder.Services.AddScoped<IAppointmentClinicRepository, AppointmentClinicRepository>();

builder.Services.AddScoped<CreateOwnerRegisterUseCase>();
builder.Services.AddScoped<GetByIdOwnerRegisterUseCase>();
builder.Services.AddScoped<GetAllOwnerRegisterUseCase>();
builder.Services.AddScoped<DeleteOwnerRegisterUseCase>();
builder.Services.AddScoped<UpdateOwnerRegisterUseCase>();

builder.Services.AddScoped<CreatePetRegisterUseCase>();
builder.Services.AddScoped<GetByIdPetRegisterUseCase>();
builder.Services.AddScoped<GetAllPetRegisterUseCase>();
builder.Services.AddScoped<DeletePetRegisterUseCase>();
builder.Services.AddScoped<UpdatePetRegisterUseCase>();

builder.Services.AddScoped<CreateAppointmentRegisterUseCase>();
builder.Services.AddScoped<GetByIdAppointmentRegisterUseCase>();
builder.Services.AddScoped<GetAllAppointmentRegisterUseCase>();
builder.Services.AddScoped<DeleteAppointmentRegisterUseCase>();
builder.Services.AddScoped<UpdateAppointmentRegisterUseCase>();

builder.Services.AddScoped<CreateVetClinicUseCase>();
builder.Services.AddScoped<GetByIdVetClinicUseCase>();
builder.Services.AddScoped<GetAllVetClinicUseCase>();
builder.Services.AddScoped<DeleteVetClinicUseCase>();
builder.Services.AddScoped<UpdateVetClinicUseCase>();

builder.Services.AddScoped<CreateAppointmentClinicUseCase>();
builder.Services.AddScoped<GetByIdAppointmentClinicUseCase>();
builder.Services.AddScoped<GetAllAppointmentClinicUseCase>();
builder.Services.AddScoped<DeleteAppointmentClinicUseCase>();
builder.Services.AddScoped<UpdateAppointmentClinicUseCase>();

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("frontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
