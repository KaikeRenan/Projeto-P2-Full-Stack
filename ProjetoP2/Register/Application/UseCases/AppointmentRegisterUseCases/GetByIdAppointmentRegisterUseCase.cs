using ProjetoP2.Register.Application.DTOs.AppointmentRegister;
using ProjetoP2.Register.Domain.IRepositories;

namespace ProjetoP2.Register.Application.UseCases.AppointmentRegisterUseCases
{
    public class GetByIdAppointmentRegisterUseCase
    {
        private readonly IAppointmentRegisterRepository _appointmentRepository;

        public GetByIdAppointmentRegisterUseCase(IAppointmentRegisterRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task<ResponseAppointmentRegisterDto> Run(Guid Id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(Id);

            if (appointment == null)
                throw new Exception("Consulta não encontrada");

            return new ResponseAppointmentRegisterDto
            {
                Id = appointment.Id,
                VetId = appointment.VetId,
                PetId = appointment.PetId,
                DateAppointment = appointment.DateAppointment,
            };
        }
    }
}
