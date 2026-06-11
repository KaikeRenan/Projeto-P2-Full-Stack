import { useState } from 'react'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { ownersApi, type Owner } from '../api/owners'
import { Button, ErrorBanner, Skeleton, EmptyState, Modal } from './ui'
import { OwnerForm } from './OwnerForm'

export function OwnerList() {
  const { data, isLoading, error } = useQuery({ queryKey: ['owners'], queryFn: ownersApi.getAll })
  const qc = useQueryClient()
  const [editing, setEditing] = useState<Owner | null>(null)
  const [creating, setCreating] = useState(false)

  const del = useMutation({
    mutationFn: ownersApi.delete,
    onMutate: async (id) => {
      await qc.cancelQueries({ queryKey: ['owners'] })
      const prev = qc.getQueryData<Owner[]>(['owners'])
      qc.setQueryData<Owner[]>(['owners'], (old) => old?.filter((o) => o.id !== id))
      return { prev }
    },
    onError: (_err, _id, ctx) => {
      if (ctx?.prev) qc.setQueryData(['owners'], ctx.prev)
    },
    onSettled: () => qc.invalidateQueries({ queryKey: ['owners'] }),
  })

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-xl font-semibold text-slate-800">Tutores</h1>
        <Button onClick={() => setCreating(true)}>+ Novo tutor</Button>
      </div>

      {isLoading && <Skeleton />}
      {error && <ErrorBanner message={(error as Error).message} />}

      {data && data.length === 0 && <EmptyState label="Nenhum tutor cadastrado. Adicione o primeiro!" />}

      {data && data.length > 0 && (
        <div className="overflow-x-auto rounded-xl border border-slate-100 shadow-sm">
          <table className="min-w-full divide-y divide-slate-100">
            <thead className="bg-slate-50">
              <tr>
                {['Nome', 'Sobrenome', 'E-mail', 'Ações'].map((h) => (
                  <th key={h} className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wider text-slate-500">
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-50 bg-white">
              {data.map((owner) => (
                <tr key={owner.id} className="hover:bg-slate-50 transition">
                  <td className="px-4 py-3 text-sm text-slate-700">{owner.firstName}</td>
                  <td className="px-4 py-3 text-sm text-slate-700">{owner.lastName}</td>
                  <td className="px-4 py-3 text-sm text-slate-500">{owner.email}</td>
                  <td className="px-4 py-3 flex gap-2">
                    <Button variant="ghost" onClick={() => setEditing(owner)}>Editar</Button>
                    <Button variant="danger" loading={del.isPending} onClick={() => del.mutate(owner.id)}>
                      Excluir
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {creating && (
        <Modal title="Novo tutor" onClose={() => setCreating(false)}>
          <OwnerForm onSuccess={() => setCreating(false)} />
        </Modal>
      )}

      {editing && (
        <Modal title="Editar tutor" onClose={() => setEditing(null)}>
          <OwnerForm defaultValues={editing} onSuccess={() => setEditing(null)} />
        </Modal>
      )}
    </div>
  )
}
