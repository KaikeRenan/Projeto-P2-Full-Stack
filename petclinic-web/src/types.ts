// ─── Tipos espelhando os DTOs de ProjetoP2 ──────────────────────────────────

// Register / Owner
export interface Owner {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}
export interface CreateOwnerDto {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  cpf: string;
}
export interface UpdateOwnerDto {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
}

// Register / Pet
export interface Pet {
  id: string;
  name: string;
  specie: string;
  sex: string;
  ownerId?: string | null;
}
export interface PetFormFields {
  name: string;
  petRG?: string | null;
  color: string;
  specie: string;
  sex: string;
  castrated: boolean;
  community: boolean;
  microchipped: boolean;
  microchippedNumber?: number | null;
  birthDate: string; // yyyy-MM-dd no form, ISO ao enviar
  state?: string | null;
  city?: string | null;
  photoURL?: string | null;
  ownerId?: string | null;
}
export type CreatePetDto = PetFormFields;
export interface UpdatePetDto extends PetFormFields {
  id: string;
}

// Register / AppointmentRegister
export interface AppointmentRegister {
  id: string;
  vetId: string;
  petId: string;
  dateAppointment: string;
}
export interface CreateAppointmentRegisterDto {
  vetId: string;
  petId: string;
  dateAppointment: string;
}
export interface UpdateAppointmentRegisterDto extends CreateAppointmentRegisterDto {
  id: string;
}

// Clinic / Vet
export interface Vet {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  cpf: string;
  crmv: string;
}
export interface CreateVetDto {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  cpf: string;
  crmv: string;
}
export interface UpdateVetDto extends CreateVetDto {
  id: string;
}

// Clinic / AppointmentClinic
export interface AppointmentClinic {
  id: string;
  vetId: string;
  petId: string;
  dateAppointment: string;
  notes?: string | null;
}
export interface CreateAppointmentClinicDto {
  vetId: string;
  petId: string;
  dateAppointment: string;
  notes?: string | null;
}
export interface UpdateAppointmentClinicDto extends CreateAppointmentClinicDto {
  id: string;
}

// ─── Constantes derivadas dos Value Objects (validações do backend) ────────

// Color.cs — HashSet exato aceito pelo backend
export const COLOR_OPTIONS = [
  "Bicolor", "Branco", "Caramelo", "Cinza", "Escaminha",
  "Frajola", "Laranja", "Marrom", "Preto", "Tigrado", "Tricolor", "Outros",
] as const;

// Sex.cs — aceita "Macho" ou "Fêmea/Femea" (normalizado sem acento)
export const SEX_OPTIONS = ["Macho", "Fêmea"] as const;

// Specie.cs — sem lista fixa no backend; sugestões via datalist
export const SPECIE_SUGGESTIONS = ["Cachorro", "Gato", "Ave", "Roedor", "Réptil", "Outro"];

// State.cs — exige >= 2 caracteres; siglas de UF do Brasil
export const UF_OPTIONS = [
  "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB",
  "PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO",
];
