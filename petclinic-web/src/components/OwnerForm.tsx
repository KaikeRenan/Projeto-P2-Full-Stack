import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { ownersApi, type Owner } from '../api/owners'
import {
  createOwnerSchema,
  updateOwnerSchema,
  type CreateOwnerValues,
  type UpdateOwnerValues,
} from '../schemas'
import { Field, Input, Button, ErrorBanner } from './ui'

interface Props {
  defaultValues?: Owner & { phoneNumber?: string }
  onSuccess: () => void
}

export function OwnerForm({ defaultValues, onSuccess }: Props) {
  const isEdit = !!defaultValues

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateOwnerValues | UpdateOwnerValues>({
    resolver: zodResolver(isEdit ? updateOwnerSchema : createOwnerSchema),
    defaultValues: defaultValues
      ? {
          id: defaultValues.id,
          firstName: defaultValues.firstName,
          lastName: defaultValues.lastName,
          email: defaultValues.email,
          phoneNumber: defaultValues.phoneNumber ?? '',
        }
      : undefined,
  })

  const qc = useQueryClient()
  const mutation = useMutation({
    mutationFn: async (data: CreateOwnerValues | UpdateOwnerValues) => {
      if (isEdit) return ownersApi.update(data as UpdateOwnerValues)
      return ownersApi.create(data as CreateOwnerValues)
    },
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ['owners'] })
      onSuccess()
    },
  })

  return (
    <form onSubmit={handleSubmit((d) => mutation.mutate(d))} className="space-y-4">
      {mutation.error && <ErrorBanner message={(mutation.error as Error).message} />}

      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <Field label="Nome" error={errors.firstName?.message}>
          <Input {...register('firstName')} error={errors.firstName?.message} placeholder="João" />
        </Field>
        <Field label="Sobrenome" error={errors.lastName?.message}>
          <Input {...register('lastName')} error={errors.lastName?.message} placeholder="Silva" />
        </Field>
      </div>

      <Field label="E-mail" error={errors.email?.message}>
        <Input {...register('email')} type="email" error={errors.email?.message} placeholder="joao@email.com" />
      </Field>

      <Field label="Telefone" error={errors.phoneNumber?.message}>
        <Input {...register('phoneNumber')} error={errors.phoneNumber?.message} placeholder="11999999999" />
      </Field>

      {!isEdit && (
        <Field label="CPF" error={(errors as { cpf?: { message?: string } }).cpf?.message}>
          <Input
            {...register('cpf')}
            error={(errors as { cpf?: { message?: string } }).cpf?.message}
            placeholder="12345678901"
            maxLength={11}
          />
        </Field>
      )}

      <div className="flex justify-end gap-3 pt-2">
        <Button type="submit" loading={mutation.isPending}>
          {isEdit ? 'Salvar alterações' : 'Cadastrar tutor'}
        </Button>
      </div>
    </form>
  )
}
