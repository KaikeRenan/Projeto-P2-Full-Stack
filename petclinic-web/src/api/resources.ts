import { api } from "./client";
import type {
  Owner, CreateOwnerDto, UpdateOwnerDto,
  Pet, CreatePetDto, UpdatePetDto,
  AppointmentRegister, CreateAppointmentRegisterDto, UpdateAppointmentRegisterDto,
  Vet, CreateVetDto, UpdateVetDto,
  AppointmentClinic, CreateAppointmentClinicDto, UpdateAppointmentClinicDto,
} from "../types";

// Fábrica genérica de chamadas REST, uma instância por controller.
// Reflete exatamente os verbos expostos: GET, GET/{id}, POST, PATCH, DELETE/{id}
function createResource<TResponse, TCreate, TUpdate extends { id: string }>(path: string) {
  return {
    getAll: async (): Promise<TResponse[]> => {
      const { data } = await api.get(path);
      return data;
    },
    getById: async (id: string): Promise<TResponse> => {
      const { data } = await api.get(`${path}/${id}`);
      return data;
    },
    create: async (dto: TCreate): Promise<TResponse> => {
      const { data } = await api.post(path, dto);
      return data;
    },
    update: async (dto: TUpdate): Promise<TResponse> => {
      const { data } = await api.patch(path, dto);
      return data;
    },
    remove: async (id: string): Promise<void> => {
      await api.delete(`${path}/${id}`);
    },
  };
}

export const ownersApi = createResource<Owner, CreateOwnerDto, UpdateOwnerDto>("/OwnerRegister");
export const petsApi = createResource<Pet, CreatePetDto, UpdatePetDto>("/PetRegister");
export const vetsApi = createResource<Vet, CreateVetDto, UpdateVetDto>("/VetClinic");
export const appointmentsRegisterApi = createResource<
  AppointmentRegister, CreateAppointmentRegisterDto, UpdateAppointmentRegisterDto
>("/AppointmentRegister");
export const appointmentsClinicApi = createResource<
  AppointmentClinic, CreateAppointmentClinicDto, UpdateAppointmentClinicDto
>("/AppointmentClinic");
