import { useForm, Controller } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { appointmentsApi, type Appointment } from '../api/appointments'
import { vetsApi } from '../api/vets'
import { petsApi } from '../api/pets'
import {
  createAppointmentSchema,
  updateAppointmentSchema,
  type CreateAppointmentValues,
  type UpdateAppointmentValues,
} from '../schemas'
import { Field, Select, Textarea, Button, ErrorBanner } from './ui'
import { Input } from './ui'

interface Props {
  defaultValues?: Appointment
  onSuccess: () => void
}

export function AppointmentForm({ defaultValues, onSuccess }: Props) {
  const isEdit = !!defaultValues
  const qc = useQueryClient()

  const { data: vets } = useQuery({ queryKey: ['vets'], queryFn: vetsApi.getAll })
  const { data: pets } = useQuery({ queryKey: ['pets'], queryFn: petsApi.getAll })

  const toDateTimeLocal = (iso?: string) => {
    if (!iso) return ''
    const d = new Date(iso)
    const pad = (n: number) => String(n).padStart(2, '0')
    return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}`
  }

  const {
    register,
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateAppointmentValues | UpdateAppointmentValues>({
    resolver: zodResolver(isEdit ? updateAppointmentSchema : createAppointmentSchema),
    defaultValues: defaultValues
      ? {
          id: defaultValues.id,
          vetId: defaultValues.vetId,
          petId: defaultValues.petId,
          dateAppointment: toDateTimeLocal(defaultValues.dateAppointment),
          notes: defaultValues.notes ?? '',
        }
      : undefined,
  })

  const mutation = useMutation({
    mutationFn: async (data: CreateAppointmentValues | UpdateAppointmentValues) => {
      const payload = {
        ...data,
        dateAppointment: new Date(data.dateAppointment).toISOString(),
        notes: data.notes || null,
      }
      if (isEdit) return appointmentsApi.update(payload as UpdateAppointmentValues)
      return appointmentsApi.create(payload as CreateAppointmentValues)
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ['appointments'] })
      onSuccess()
    },
  })

  const err = errors as Record<string, { message?: string }>

  return (
    <form onSubmit={handleSubmit((d) => mutation.mutate(d))} className="space-y-4">
      {mutation.error && <ErrorBanner message={(mutation.error as Error).message} />}

      <Field label="Veterinário" error={err.vetId?.message}>
        <Controller
          control={control}
          name="vetId"
          render={({ field }) => (
            <Select {...field} error={err.vetId?.message}>
              <option value="">Selecione um veterinário</option>
              {vets?.map((v) => (
                <option key={v.id} value={v.id}>
                  {v.firstName} {v.lastName} — {v.crmv}
                </option>
              ))}
            </Select>
          )}
        />
      </Field>

      <Field label="Pet" error={err.petId?.message}>
        <Controller
          control={control}
          name="petId"
          render={({ field }) => (
            <Select {...field} error={err.petId?.message}>
              <option value="">Selecione um pet</option>
              {pets?.map((p) => (
                <option key={p.id} value={p.id}>
                  {p.name} ({p.specie})
                </option>
              ))}
            </Select>
          )}
        />
      </Field>

      <Field label="Data e hora" error={err.dateAppointment?.message}>
        <Input
          {...register('dateAppointment')}
          type="datetime-local"
          error={err.dateAppointment?.message}
        />
      </Field>

      <Field label="Observações (opcional)" error={err.notes?.message}>
        <Textarea {...register('notes')} error={err.notes?.message} placeholder="Sintomas, diagnóstico..." />
      </Field>

      <div className="flex justify-end pt-2">
        <Button type="submit" loading={mutation.isPending}>
          {isEdit ? 'Salvar alterações' : 'Agendar consulta'}
        </Button>
      </div>
    </form>
  )
}
