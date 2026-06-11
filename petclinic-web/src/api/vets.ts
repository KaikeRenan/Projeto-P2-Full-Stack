import { api } from './client'

export interface Vet {
  id: string
  firstName: string
  lastName: string
  email: string
  phoneNumber: string
  cpf: string
  crmv: string
}

export interface CreateVetDto {
  firstName: string
  lastName: string
  email: string
  phoneNumber: string
  cpf: string
  crmv: string
}

export interface UpdateVetDto extends CreateVetDto {
  id: string
}

export const vetsApi = {
  getAll: () => api.get<Vet[]>('/VetClinic').then((r) => r.data),
  getById: (id: string) => api.get<Vet>(`/VetClinic/${id}`).then((r) => r.data),
  create: (dto: CreateVetDto) => api.post<Vet>('/VetClinic', dto).then((r) => r.data),
  update: (dto: UpdateVetDto) => api.patch<Vet>('/VetClinic', dto).then((r) => r.data),
  delete: (id: string) => api.delete(`/VetClinic/${id}`),
}
