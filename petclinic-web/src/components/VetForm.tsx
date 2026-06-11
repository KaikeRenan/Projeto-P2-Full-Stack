import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { vetsApi, type Vet } from '../api/vets'
import {
  createVetSchema,
  updateVetSchema,
  type CreateVetValues,
  type UpdateVetValues,
} from '../schemas'
import { Field, Input, Button, ErrorBanner } from './ui'

interface Props {
  defaultValues?: Vet
  onSuccess: () => void
}

export function VetForm({ defaultValues, onSuccess }: Props) {
  const isEdit = !!defaultValues
  const qc = useQueryClient()

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateVetValues | UpdateVetValues>({
    resolver: zodResolver(isEdit ? updateVetSchema : createVetSchema),
    defaultValues: defaultValues ?? undefined,
  })

  const mutation = useMutation({
    mutationFn: async (data: CreateVetValues | UpdateVetValues) => {
      if (isEdit) return vetsApi.update(data as UpdateVetValues)
      return vetsApi.create(data as CreateVetValues)
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ['vets'] })
      onSuccess()
    },
  })

  const err = errors as Record<string, { message?: string }>

  return (
    <form onSubmit={handleSubmit((d) => mutation.mutate(d))} className="space-y-4">
      {mutation.error && <ErrorBanner message={(mutation.error as Error).message} />}

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <Field label="Nome" error={err.firstName?.message}>
          <Input {...register('firstName')} error={err.firstName?.message} placeholder="Ana" />
        </Field>
        <Field label="Sobrenome" error={err.lastName?.message}>
          <Input {...register('lastName')} error={err.lastName?.message} placeholder="Souza" />
        </Field>
      </div>

      <Field label="E-mail" error={err.email?.message}>
        <Input {...register('email')} type="email" error={err.email?.message} placeholder="ana@clinica.com" />
      </Field>

      <Field label="Telefone" error={err.phoneNumber?.message}>
        <Input {...register('phoneNumber')} error={err.phoneNumber?.message} placeholder="11999999999" />
      </Field>

      <Field label="CPF" error={err.cpf?.message}>
        <Input {...register('cpf')} error={err.cpf?.message} placeholder="12345678901" maxLength={11} />
      </Field>

      <Field label="CRMV" error={err.crmv?.message}>
        <Input {...register('crmv')} error={err.crmv?.message} placeholder="12345-SP" />
      </Field>

      <div className="flex justify-end pt-2">
        <Button type="submit" loading={mutation.isPending}>
          {isEdit ? 'Salvar alterações' : 'Cadastrar veterinário'}
        </Button>
      </div>
    </form>
  )
}
