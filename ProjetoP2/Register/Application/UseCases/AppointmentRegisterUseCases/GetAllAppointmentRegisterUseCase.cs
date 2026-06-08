using ProjetoP2.Register.Application.DTOs.AppointmentRegister;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.AppointmentRegisterUseCases
{
    public class GetAllAppointmentRegisterUseCase
    {
        private readonly IAppointmentRegisterRepository _appointmentRepository;

        public GetAllAppointmentRegisterUseCase(IAppointmentRegisterRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task<List<ResponseAppointmentRegisterDto>> Run()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            return appointments.Select(appointment => new ResponseAppointmentRegisterDto
            {
                Id = appointment.Id,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment,
            }).ToList();
        }
    }
}
