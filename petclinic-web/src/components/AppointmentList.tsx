import { useState } from 'react'
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { appointmentsApi, type Appointment } from '../api/appointments'
import { Button, ErrorBanner, Skeleton, EmptyState, Modal } from './ui'
import { AppointmentForm } from './AppointmentForm'

export function AppointmentList() {
  const { data, isLoading, error } = useQuery({
    queryKey: ['appointments'],
    queryFn: appointmentsApi.getAll,
  })
  const qc = useQueryClient()
  const [editing, setEditing] = useState<Appointment | null>(null)
  const [creating, setCreating] = useState(false)

  const del = useMutation({
    mutationFn: appointmentsApi.delete,
    onMutate: async (id) => {
      await qc.cancelQueries({ queryKey: ['appointments'] })
      const prev = qc.getQueryData<Appointment[]>(['appointments'])
      qc.setQueryData<Appointment[]>(['appointments'], (old) => old?.filter((a) => a.id !== id))
      return { prev }
    },
    onError: (_err, _id, ctx) => {
      if (ctx?.prev) qc.setQueryData(['appointments'], ctx.prev)
    },
    onSettled: () => qc.invalidateQueries({ queryKey: ['appointments'] }),
  })

  const fmt = (iso: string) =>
    new Date(iso).toLocaleString('pt-BR', { dateStyle: 'short', timeStyle: 'short' })

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-xl font-semibold text-slate-800">Consultas</h1>
        <Button onClick={() => setCreating(true)}>+ Nova consulta</Button>
      </div>

      {isLoading && <Skeleton />}
      {error && <ErrorBanner message={(error as Error).message} />}
      {data && data.length === 0 && <EmptyState label="Nenhuma consulta agendada." />}

      {data && data.length > 0 && (
        <div className="overflow-x-auto rounded-xl border border-slate-100 shadow-sm">
          <table className="min-w-full divide-y divide-slate-100">
            <thead className="bg-slate-50">
              <tr>
                {['Data', 'Vet ID', 'Pet ID', 'Observações', 'Ações'].map((h) => (
                  <th key={h} className="px-4 py-3 text-left text-xs font-semibold uppercase tracking-wider text-slate-500">
                    {h}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-50 bg-white">
              {data.map((appt) => (
                <tr key={appt.id} className="hover:bg-slate-50 transition">
                  <td className="px-4 py-3 text-sm font-medium text-slate-800">{fmt(appt.dateAppointment)}</td>
                  <td className="px-4 py-3 text-xs text-slate-400 max-w-[100px] truncate font-mono">{appt.vetId}</td>
                  <td className="px-4 py-3 text-xs text-slate-400 max-w-[100px] truncate font-mono">{appt.petId}</td>
                  <td className="px-4 py-3 text-sm text-slate-500 max-w-[200px] truncate">{appt.notes ?? '—'}</td>
                  <td className="px-4 py-3 flex gap-2">
                    <Button variant="ghost" onClick={() => setEditing(appt)}>Editar</Button>
                    <Button variant="danger" loading={del.isPending} onClick={() => del.mutate(appt.id)}>
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
        <Modal title="Nova consulta" onClose={() => setCreating(false)}>
          <AppointmentForm onSuccess={() => setCreating(false)} />
        </Modal>
      )}

      {editing && (
        <Modal title="Editar consulta" onClose={() => setEditing(null)}>
          <AppointmentForm defaultValues={editing} onSuccess={() => setEditing(null)} />
        </Modal>
      )}
    </div>
  )
}
