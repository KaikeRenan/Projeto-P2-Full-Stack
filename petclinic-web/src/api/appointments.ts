import { api } from './client'

export interface Appointment {
  id: string
  vetId: string
  petId: string
  dateAppointment: string
  notes?: string | null
}

export interface CreateAppointmentDto {
  vetId: string
  petId: string
  dateAppointment: string
  notes?: string | null
}

export interface UpdateAppointmentDto extends CreateAppointmentDto {
  id: string
}

export const appointmentsApi = {
  getAll: () => api.get<Appointment[]>('/AppointmentClinic').then((r) => r.data),
  getById: (id: string) => api.get<Appointment>(`/AppointmentClinic/${id}`).then((r) => r.data),
  create: (dto: CreateAppointmentDto) =>
    api.post<Appointment>('/AppointmentClinic', dto).then((r) => r.data),
  update: (dto: UpdateAppointmentDto) =>
    api.patch<Appointment>('/AppointmentClinic', dto).then((r) => r.data),
  delete: (id: string) => api.delete(`/AppointmentClinic/${id}`),
}
