import { useState } from 'react'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { petsApi, type Pet } from '../api/pets'
import { Button, ErrorBanner, Skeleton, EmptyState, Modal } from './ui'
import { PetForm } from './PetForm'

export function PetList() {
  const { data, isLoading, error } = useQuery({ queryKey: ['pets'], queryFn: petsApi.getAll })
  const qc = useQueryClient()
  const [editing, setEditing] = useState<Pet | null>(null)
  const [creating, setCreating] = useState(false)

  const del = useMutation({
    mutationFn: petsApi.delete,
    onMutate: async (id) => {
      await qc.cancelQueries({ queryKey: ['pets'] })
      const prev = qc.getQueryData<Pet[]>(['pets'])
      qc.setQueryData<Pet[]>(['pets'], (old) => old?.filter((p) => p.id !== id))
      return { prev }
    },
    onError: (_err, _id, ctx) => {
      if (ctx?.prev) qc.setQueryData(['pets'], ctx.prev)
    },
    onSettled: () => qc.invalidateQueries({ queryKey: ['pets'] }),
  })

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-xl font-semibold text-slate-800">Pets</h1>
        <Button onClick={() => setCreating(true)}>+ Novo pet</Button>
      </div>

      {isLoading && <Skeleton />}
      {error && <ErrorBanner message={(error as Error).message} />}
      {data && data.length === 0 && <EmptyState label="Nenhum pet cadastrado. Adicione o primeiro!" />}

      {data && data.length > 0 && (
        <div className="overflow-x-auto rounded-xl border border-slate-100 shadow-sm">
          <table className="min-w-full divide-y divide-slate-100">
            <thead className="bg-slate-50">
              <tr>
                {['Nome', 'Espécie', 'Sexo', 'Tutor ID', 'Ações'].map((h) => (
                  <th key={h} className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wider text-slate-500">
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-50 bg-white">
              {data.map((pet) => (
                <tr key={pet.id} className="hover:bg-slate-50 transition">
                  <td className="px-4 py-3 text-sm font-medium text-slate-800">{pet.name}</td>
                  <td className="px-4 py-3 text-sm text-slate-600">{pet.specie}</td>
                  <td className="px-4 py-3 text-sm text-slate-600">{pet.sex}</td>
                  <td className="px-4 py-3 text-xs text-slate-400 max-w-[120px] truncate">{pet.ownerId ?? '—'}</td>
                  <td className="px-4 py-3 flex gap-2">
                    <Button variant="ghost" onClick={() => setEditing(pet)}>Editar</Button>
                    <Button variant="danger" loading={del.isPending} onClick={() => del.mutate(pet.id)}>
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
        <Modal title="Novo pet" onClose={() => setCreating(false)}>
          <PetForm onSuccess={() => setCreating(false)} />
        </Modal>
      )}

      {editing && (
        <Modal title="Editar pet" onClose={() => setEditing(null)}>
          <PetForm defaultValues={editing} onSuccess={() => setEditing(null)} />
        </Modal>
      )}
    </div>
  )
}
