import { api } from './client'

export interface Pet {
  id: string
  name: string
  specie: string
  sex: string
  ownerId?: string | null
}

export interface CreatePetDto {
  name: string
  petRG?: string | null
  color: string
  specie: string
  sex: string
  castrated: boolean
  community: boolean
  microchipped: boolean
  microchippedNumber?: number | null
  birthDate: string
  state?: string | null
  city?: string | null
  photoURL?: string | null
  ownerId?: string | null
}

export interface UpdatePetDto extends CreatePetDto {
  id: string
}

export const petsApi = {
  getAll: () => api.get<Pet[]>('/PetRegister').then((r) => r.data),
  getById: (id: string) => api.get<Pet>(`/PetRegister/${id}`).then((r) => r.data),
  create: (dto: CreatePetDto) => api.post<Pet>('/PetRegister', dto).then((r) => r.data),
  update: (dto: UpdatePetDto) => api.patch<Pet>('/PetRegister', dto).then((r) => r.data),
  delete: (id: string) => api.delete(`/PetRegister/${id}`),
}
