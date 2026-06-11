import { z } from 'zod'

// ── Owner ──────────────────────────────────────────────────────────────────
export const createOwnerSchema = z.object({
  firstName: z.string().min(1, 'Nome obrigatório'),
  lastName: z.string().min(1, 'Sobrenome obrigatório'),
  email: z.string().email('E-mail inválido'),
  phoneNumber: z.string().min(10, 'Telefone deve ter ao menos 10 dígitos'),
  cpf: z.string().length(11, 'CPF deve ter exatamente 11 dígitos').regex(/^\d+$/, 'CPF deve conter apenas números'),
})

export const updateOwnerSchema = createOwnerSchema
  .omit({ cpf: true })
  .extend({ id: z.string().uuid() })

export type CreateOwnerValues = z.infer<typeof createOwnerSchema>
export type UpdateOwnerValues = z.infer<typeof updateOwnerSchema>

// ── Pet ────────────────────────────────────────────────────────────────────
const SEX_OPTIONS = ['Macho', 'Fêmea'] as const
const COLOR_OPTIONS = [
  'Bicolor','Branco','Caramelo','Cinza','Escaminha',
  'Frajola','Laranja','Marrom','Preto','Tigrado','Tricolor','Outros',
] as const

const petBaseSchema = z.object({
  name: z.string().min(1, 'Nome obrigatório'),
  petRG: z.string().min(15, 'RGA deve ter ao menos 15 caracteres').nullable().optional(),
  color: z.enum(COLOR_OPTIONS, {
    errorMap: () => ({ message: 'Cor inválida' }),
  }),
  specie: z.string().min(1, 'Espécie obrigatória'),
  sex: z.enum(SEX_OPTIONS, {
    errorMap: () => ({ message: 'Sexo inválido' }),
  }),
  castrated: z.boolean(),
  community: z.boolean(),
  microchipped: z.boolean(),
  microchippedNumber: z.number().int().max(999999999999999).nullable().optional(),
  birthDate: z.string().min(1, 'Data de nascimento obrigatória'),
  state: z.string().min(2, 'Sigla de estado inválida').nullable().optional(),
  city: z.string().min(1, 'Cidade inválida').nullable().optional(),
  photoURL: z.string().url('URL inválida').nullable().optional().or(z.literal('')),
  ownerId: z.string().uuid().nullable().optional(),
})

export const createPetSchema = petBaseSchema.superRefine((data, ctx) => {
  if (data.microchipped && !data.microchippedNumber) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      path: ['microchippedNumber'],
      message: 'Número do microchip obrigatório quando microchipado',
    })
  }
})

export const updatePetSchema = petBaseSchema
  .extend({
    id: z.string().uuid(),
  })
  .superRefine((data, ctx) => {
    if (data.microchipped && !data.microchippedNumber) {
      ctx.addIssue({
        code: z.ZodIssueCode.custom,
        path: ['microchippedNumber'],
        message: 'Número do microchip obrigatório quando microchipado',
      })
    }
  })

export type CreatePetValues = z.infer<typeof createPetSchema>
export type UpdatePetValues = z.infer<typeof updatePetSchema>

export { SEX_OPTIONS, COLOR_OPTIONS }

// ── Vet ────────────────────────────────────────────────────────────────────
export const createVetSchema = z.object({
  firstName: z.string().min(1, 'Nome obrigatório'),
  lastName: z.string().min(1, 'Sobrenome obrigatório'),
  email: z.string().email('E-mail inválido'),
  phoneNumber: z.string().min(10, 'Telefone deve ter ao menos 10 dígitos'),
  cpf: z.string().length(11, 'CPF deve ter exatamente 11 dígitos').regex(/^\d+$/, 'CPF deve conter apenas números'),
  crmv: z
    .string()
    .min(1, 'CRMV obrigatório')
    .regex(/^\d{4,6}-[A-Za-z]{2}$/, 'CRMV deve seguir o formato 12345-SP'),
})

export const updateVetSchema = createVetSchema.extend({ id: z.string().uuid() })

export type CreateVetValues = z.infer<typeof createVetSchema>
export type UpdateVetValues = z.infer<typeof updateVetSchema>

// ── Appointment ────────────────────────────────────────────────────────────
export const createAppointmentSchema = z.object({
  vetId: z.string().uuid('Veterinário obrigatório'),
  petId: z.string().uuid('Pet obrigatório'),
  dateAppointment: z.string().min(1, 'Data obrigatória'),
  notes: z.string().nullable().optional(),
})

export const updateAppointmentSchema = createAppointmentSchema.extend({ id: z.string().uuid() })

export type CreateAppointmentValues = z.infer<typeof createAppointmentSchema>
export type UpdateAppointmentValues = z.infer<typeof updateAppointmentSchema>
