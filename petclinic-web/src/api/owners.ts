import { api } from './client'

export interface Owner {
  id: string
  firstName: string
  lastName: string
  email: string
}

export interface CreateOwnerDto {
  firstName: string
  lastName: string
  email: string
  phoneNumber: string
  cpf: string
}

export interface UpdateOwnerDto {
  id: string
  firstName: string
  lastName: string
  email: string
  phoneNumber: string
}

export const ownersApi = {
  getAll: () => api.get<Owner[]>('/OwnerRegister').then((r) => r.data),
  getById: (id: string) => api.get<Owner>(`/OwnerRegister/${id}`).then((r) => r.data),
  create: (dto: CreateOwnerDto) => api.post<Owner>('/OwnerRegister', dto).then((r) => r.data),
  update: (dto: UpdateOwnerDto) => api.patch<Owner>('/OwnerRegister', dto).then((r) => r.data),
  delete: (id: string) => api.delete(`/OwnerRegister/${id}`),
}
