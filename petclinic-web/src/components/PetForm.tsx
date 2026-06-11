import { useForm, Controller } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { petsApi, type Pet } from '../api/pets'
import { ownersApi } from '../api/owners'
import {
  createPetSchema,
  updatePetSchema,
  type CreatePetValues,
  type UpdatePetValues,
  SEX_OPTIONS,
  COLOR_OPTIONS,
} from '../schemas'
import { Field, Input, Select, Button, ErrorBanner } from './ui'

interface Props {
  defaultValues?: Pet
  onSuccess: () => void
}

export function PetForm({ defaultValues, onSuccess }: Props) {
  const isEdit = !!defaultValues
  const qc = useQueryClient()

  const { data: owners } = useQuery({ queryKey: ['owners'], queryFn: ownersApi.getAll })

  const {
    register,
    control,
    watch,
    handleSubmit,
    formState: { errors },
  } = useForm<CreatePetValues | UpdatePetValues>({
    resolver: zodResolver(isEdit ? updatePetSchema : createPetSchema),
    defaultValues: defaultValues
      ? {
          id: defaultValues.id,
          name: defaultValues.name,
          specie: defaultValues.specie,
          sex: defaultValues.sex as 'Macho' | 'Fêmea',
          ownerId: defaultValues.ownerId ?? null,
          color: 'Outros' as const,
          castrated: false,
          community: false,
          microchipped: false,
          birthDate: '',
        }
      : {
          castrated: false,
          community: false,
          microchipped: false,
        },
  })

  const microchipped = watch('microchipped')

  const mutation = useMutation({
    mutationFn: async (data: CreatePetValues | UpdatePetValues) => {
      const payload = {
        ...data,
        birthDate: data.birthDate ? new Date(data.birthDate).toISOString() : '',
        microchippedNumber: data.microchipped ? (data.microchippedNumber ?? null) : null,
        photoURL: data.photoURL || null,
      }
      if (isEdit) return petsApi.update(payload as UpdatePetValues)
      return petsApi.create(payload as CreatePetValues)
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ['pets'] })
      onSuccess()
    },
  })

  const err = errors as Record<string, { message?: string }>

  return (
    <form onSubmit={handleSubmit((d) => mutation.mutate(d))} className="space-y-4">
      {mutation.error && <ErrorBanner message={(mutation.error as Error).message} />}

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <Field label="Nome" error={err.name?.message}>
          <Input {...register('name')} error={err.name?.message} placeholder="Rex" />
        </Field>
        <Field label="Espécie" error={err.specie?.message}>
          <Input {...register('specie')} error={err.specie?.message} placeholder="Cão, Gato..." />
        </Field>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <Field label="Sexo" error={err.sex?.message}>
          <Controller
            control={control}
            name="sex"
            render={({ field }) => (
              <Select {...field} error={err.sex?.message}>
                <option value="">Selecione</option>
                {SEX_OPTIONS.map((s) => <option key={s} value={s}>{s}</option>)}
              </Select>
            )}
          />
        </Field>
        <Field label="Cor" error={err.color?.message}>
          <Controller
            control={control}
            name="color"
            render={({ field }) => (
              <Select {...field} error={err.color?.message}>
                <option value="">Selecione</option>
                {COLOR_OPTIONS.map((c) => <option key={c} value={c}>{c}</option>)}
              </Select>
            )}
          />
        </Field>
      </div>

      <Field label="Data de nascimento" error={err.birthDate?.message}>
        <Input {...register('birthDate')} type="date" error={err.birthDate?.message} />
      </Field>

      <Field label="RGA (opcional)" error={err.petRG?.message}>
        <Input {...register('petRG')} error={err.petRG?.message} placeholder="Mínimo 15 caracteres" />
      </Field>

      <div className="grid grid-cols-3 gap-4">
        {(['castrated', 'community', 'microchipped'] as const).map((field) => (
          <label key={field} className="flex items-center gap-2 cursor-pointer select-none">
            <Controller
              control={control}
              name={field}
              render={({ field: f }) => (
                <input
                  type="checkbox"
                  checked={f.value as boolean}
                  onChange={f.onChange}
                  className="h-4 w-4 rounded border-slate-300 text-teal-600 focus:ring-teal-500"
                />
              )}
            />
            <span className="text-sm text-slate-700 capitalize">
              {field === 'castrated' ? 'Castrado' : field === 'community' ? 'Comunitário' : 'Microchipado'}
            </span>
          </label>
        ))}
      </div>

      {microchipped && (
        <Field label="Número do microchip" error={err.microchippedNumber?.message}>
          <Input
            {...register('microchippedNumber', { valueAsNumber: true })}
            type="number"
            error={err.microchippedNumber?.message}
            placeholder="Ex: 985141002012345"
          />
        </Field>
      )}

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <Field label="Estado (UF)" error={err.state?.message}>
          <Input {...register('state')} error={err.state?.message} placeholder="SP" maxLength={2} />
        </Field>
        <Field label="Cidade" error={err.city?.message}>
          <Input {...register('city')} error={err.city?.message} placeholder="São Paulo" />
        </Field>
      </div>

      <Field label="URL da foto (opcional)" error={err.photoURL?.message}>
        <Input {...register('photoURL')} error={err.photoURL?.message} placeholder="https://..." />
      </Field>

      <Field label="Tutor (opcional)" error={err.ownerId?.message}>
        <Controller
          control={control}
          name="ownerId"
          render={({ field }) => (
            <Select
              value={field.value ?? ''}
              onChange={(e) => field.onChange(e.target.value || null)}
              error={err.ownerId?.message}
            >
              <option value="">Sem tutor</option>
              {owners?.map((o) => (
                <option key={o.id} value={o.id}>
                  {o.firstName} {o.lastName}
                </option>
              ))}
            </Select>
          )}
        />
      </Field>

      <div className="flex justify-end pt-2">
        <Button type="submit" loading={mutation.isPending}>
          {isEdit ? 'Salvar alterações' : 'Cadastrar pet'}
        </Button>
      </div>
    </form>
  )
}
