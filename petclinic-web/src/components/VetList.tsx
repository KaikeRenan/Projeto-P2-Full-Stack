import { useState } from 'react'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { vetsApi, type Vet } from '../api/vets'
import { Button, ErrorBanner, Skeleton, EmptyState, Modal } from './ui'
import { VetForm } from './VetForm'

export function VetList() {
  const { data, isLoading, error } = useQuery({ queryKey: ['vets'], queryFn: vetsApi.getAll })
  const qc = useQueryClient()
  const [editing, setEditing] = useState<Vet | null>(null)
  const [creating, setCreating] = useState(false)

  const del = useMutation({
    mutationFn: vetsApi.delete,
    onMutate: async (id) => {
      await qc.cancelQueries({ queryKey: ['vets'] })
      const prev = qc.getQueryData<Vet[]>(['vets'])
      qc.setQueryData<Vet[]>(['vets'], (old) => old?.filter((v) => v.id !== id))
      return { prev }
    },
    onError: (_err, _id, ctx) => {
      if (ctx?.prev) qc.setQueryData(['vets'], ctx.prev)
    },
    onSettled: () => qc.invalidateQueries({ queryKey: ['vets'] }),
  })

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-xl font-semibold text-slate-800">Veterinários</h1>
        <Button onClick={() => setCreating(true)}>+ Novo veterinário</Button>
      </div>

      {isLoading && <Skeleton />}
      {error && <ErrorBanner message={(error as Error).message} />}
      {data && data.length === 0 && <EmptyState label="Nenhum veterinário cadastrado. Adicione o primeiro!" />}

      {data && data.length > 0 && (
        <div className="overflow-x-auto rounded-xl border border-slate-100 shadow-sm">
          <table className="min-w-full divide-y divide-slate-100">
            <thead className="bg-slate-50">
              <tr>
                {['Nome', 'E-mail', 'CRMV', 'CPF', 'Ações'].map((h) => (
                  <th key={h} className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wider text-slate-500">
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-50 bg-white">
              {data.map((vet) => (
                <tr key={vet.id} className="hover:bg-slate-50 transition">
                  <td className="px-4 py-3 text-sm font-medium text-slate-800">{vet.firstName} {vet.lastName}</td>
                  <td className="px-4 py-3 text-sm text-slate-500">{vet.email}</td>
                  <td className="px-4 py-3 text-sm text-teal-700 font-mono">{vet.crmv}</td>
                  <td className="px-4 py-3 text-sm text-slate-500 font-mono">{vet.cpf}</td>
                  <td className="px-4 py-3 flex gap-2">
                    <Button variant="ghost" onClick={() => setEditing(vet)}>Editar</Button>
                    <Button variant="danger" loading={del.isPending} onClick={() => del.mutate(vet.id)}>
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
        <Modal title="Novo veterinário" onClose={() => setCreating(false)}>
          <VetForm onSuccess={() => setCreating(false)} />
        </Modal>
      )}

      {editing && (
        <Modal title="Editar veterinário" onClose={() => setEditing(null)}>
          <VetForm defaultValues={editing} onSuccess={() => setEditing(null)} />
        </Modal>
      )}
    </div>
  )
}
