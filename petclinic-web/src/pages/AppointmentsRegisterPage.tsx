import { useEffect, useState } from "react";
import { appointmentsRegisterApi, petsApi, vetsApi } from "../api/resources";
import { useResource } from "../hooks/useResource";
import type { AppointmentRegister, CreateAppointmentRegisterDto, Pet, UpdateAppointmentRegisterDto, Vet } from "../types";
import { PageHeader } from "../components/PageHeader";
import { EmptyState } from "../components/EmptyState";
import { ErrorState } from "../components/ErrorState";
import { Skeleton } from "../components/Skeleton";
import { Modal } from "../components/Modal";
import { ConfirmDialog } from "../components/ConfirmDialog";
import { TextField, SelectField } from "../components/FormFields";
import { EditIcon, TrashIcon } from "../components/icons";
import { useToast } from "../components/Toast";

const emptyForm: CreateAppointmentRegisterDto = { vetId: "", petId: "", dateAppointment: "" };

function toLocalInputValue(iso: string) {
  if (!iso) return "";
  const d = new Date(iso);
  const pad = (n: number) => String(n).padStart(2, "0");
  return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}`;
}

function formatDate(iso: string) {
  return new Date(iso).toLocaleString("pt-BR", { dateStyle: "short", timeStyle: "short" });
}

export default function AppointmentsRegisterPage() {
  const { items, loading, error, reload, create, update, remove } = useResource(appointmentsRegisterApi);
  const { notify } = useToast();

  const [pets, setPets] = useState<Pet[]>([]);
  const [vets, setVets] = useState<Vet[]>([]);
  useEffect(() => {
    petsApi.getAll().then(setPets).catch(() => setPets([]));
    vetsApi.getAll().then(setVets).catch(() => setVets([]));
  }, []);

  const petName = (id: string) => pets.find((p) => p.id === id)?.name ?? id;
  const vetName = (id: string) => {
    const v = vets.find((x) => x.id === id);
    return v ? `${v.firstName} ${v.lastName}` : id;
  };

  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<AppointmentRegister | null>(null);
  const [form, setForm] = useState<CreateAppointmentRegisterDto>(emptyForm);
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);
  const [toRemove, setToRemove] = useState<AppointmentRegister | null>(null);
  const [removing, setRemoving] = useState(false);

  const openCreate = () => {
    setEditing(null);
    setForm(emptyForm);
    setFormError(null);
    setModalOpen(true);
  };

  const openEdit = (appt: AppointmentRegister) => {
    setEditing(appt);
    setForm({
      vetId: appt.vetId,
      petId: appt.petId,
      dateAppointment: toLocalInputValue(appt.dateAppointment),
    });
    setFormError(null);
    setModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setFormError(null);
    setSubmitting(true);
    try {
      const payload = { ...form, dateAppointment: new Date(form.dateAppointment).toISOString() };
      if (editing) {
        const dto: UpdateAppointmentRegisterDto = { id: editing.id, ...payload };
        await update(dto);
      } else {
        await create(payload);
      }
      setModalOpen(false);
    } catch (err) {
      setFormError(err instanceof Error ? err.message : "Erro ao salvar");
    } finally {
      setSubmitting(false);
    }
  };

  const handleRemove = async () => {
    if (!toRemove) return;
    setRemoving(true);
    try {
      await remove(toRemove.id);
      setToRemove(null);
    } catch (err) {
      notify(err instanceof Error ? err.message : "Erro ao remover", "error");
    } finally {
      setRemoving(false);
    }
  };

  return (
    <div>
      <PageHeader
        title="Consultas (Cadastro)"
        description="Agendamentos vinculados ao módulo de Cadastro — sem campo de observações."
        actionLabel="Nova consulta"
        onAction={openCreate}
      />

      {loading && <Skeleton />}
      {!loading && error && <ErrorState message={error} onRetry={reload} />}
      {!loading && !error && items.length === 0 && (
        <EmptyState message="Nenhuma consulta agendada neste módulo ainda." />
      )}

      {!loading && !error && items.length > 0 && (
        <div className="grid gap-3 sm:grid-cols-2">
          {items.map((appt) => (
            <div key={appt.id} className="record-card" style={{ ["--strip-color" as string]: "#3F6FD1" }}>
              <div className="flex items-start justify-between gap-2">
                <div>
                  <p className="font-display font-semibold text-base">{petName(appt.petId)}</p>
                  <p className="text-sm text-muted">com {vetName(appt.vetId)}</p>
                  <p className="text-xs font-mono text-muted/80 mt-1">{formatDate(appt.dateAppointment)}</p>
                </div>
                <div className="flex gap-1 shrink-0">
                  <button onClick={() => openEdit(appt)} aria-label="Editar"
                    className="p-1.5 rounded-lg text-muted hover:text-primary hover:bg-primary-light transition-colors">
                    <EditIcon className="w-4 h-4" />
                  </button>
                  <button onClick={() => setToRemove(appt)} aria-label="Remover"
                    className="p-1.5 rounded-lg text-muted hover:text-accent hover:bg-accent-light transition-colors">
                    <TrashIcon className="w-4 h-4" />
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      <Modal title={editing ? "Editar consulta" : "Nova consulta"} open={modalOpen} onClose={() => setModalOpen(false)}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <SelectField label="Pet" htmlFor="petId" value={form.petId} required placeholder="Selecione o pet"
            options={pets.map((p) => ({ value: p.id, label: p.name }))}
            onChange={(v) => setForm((f) => ({ ...f, petId: v }))} />

          <SelectField label="Veterinário" htmlFor="vetId" value={form.vetId} required placeholder="Selecione o veterinário"
            options={vets.map((v) => ({ value: v.id, label: `${v.firstName} ${v.lastName}` }))}
            onChange={(v) => setForm((f) => ({ ...f, vetId: v }))} />

          <TextField label="Data e hora" htmlFor="dateAppointment" type="datetime-local" value={form.dateAppointment} required
            onChange={(v) => setForm((f) => ({ ...f, dateAppointment: v }))} />

          {formError && <p className="field-error">{formError}</p>}

          <div className="flex justify-end gap-2 pt-2">
            <button type="button" className="btn-ghost" onClick={() => setModalOpen(false)} disabled={submitting}>
              Cancelar
            </button>
            <button type="submit" className="btn-primary" disabled={submitting}>
              {submitting ? "Salvando..." : "Salvar"}
            </button>
          </div>
        </form>
      </Modal>

      <ConfirmDialog
        open={!!toRemove}
        title="Remover consulta"
        description="Remover este agendamento? Esta ação marca o registro como removido."
        onCancel={() => setToRemove(null)}
        onConfirm={handleRemove}
        loading={removing}
      />
    </div>
  );
}
