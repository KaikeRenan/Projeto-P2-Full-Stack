import { useState } from "react";
import { vetsApi } from "../api/resources";
import { useResource } from "../hooks/useResource";
import type { CreateVetDto, UpdateVetDto, Vet } from "../types";
import { PageHeader } from "../components/PageHeader";
import { EmptyState } from "../components/EmptyState";
import { ErrorState } from "../components/ErrorState";
import { Skeleton } from "../components/Skeleton";
import { Modal } from "../components/Modal";
import { ConfirmDialog } from "../components/ConfirmDialog";
import { TextField } from "../components/FormFields";
import { EditIcon, TrashIcon } from "../components/icons";
import { useToast } from "../components/Toast";

const emptyForm: CreateVetDto = {
  firstName: "", lastName: "", email: "", phoneNumber: "", cpf: "", crmv: "",
};

// CRMV.cs exige o formato NNNNN-UF (4 a 6 dígitos, traço, 2 letras)
const CRMV_PATTERN = /^\d{4,6}-[A-Za-z]{2}$/;

export default function VetsPage() {
  const { items, loading, error, reload, create, update, remove } = useResource(vetsApi);
  const { notify } = useToast();

  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Vet | null>(null);
  const [form, setForm] = useState<CreateVetDto>(emptyForm);
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);
  const [toRemove, setToRemove] = useState<Vet | null>(null);
  const [removing, setRemoving] = useState(false);

  const openCreate = () => {
    setEditing(null);
    setForm(emptyForm);
    setFormError(null);
    setModalOpen(true);
  };

  const openEdit = (vet: Vet) => {
    setEditing(vet);
    setForm({
      firstName: vet.firstName,
      lastName: vet.lastName,
      email: vet.email,
      phoneNumber: vet.phoneNumber,
      cpf: vet.cpf,
      crmv: vet.crmv,
    });
    setFormError(null);
    setModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setFormError(null);

    if (!CRMV_PATTERN.test(form.crmv.trim())) {
      setFormError("CRMV deve seguir o formato 12345-UF");
      return;
    }

    setSubmitting(true);
    try {
      if (editing) {
        const dto: UpdateVetDto = { id: editing.id, ...form };
        await update(dto);
      } else {
        await create(form);
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
        title="Veterinários"
        description="Profissionais habilitados a registrar consultas na Clínica."
        actionLabel="Novo veterinário"
        onAction={openCreate}
      />

      {loading && <Skeleton />}
      {!loading && error && <ErrorState message={error} onRetry={reload} />}
      {!loading && !error && items.length === 0 && (
        <EmptyState message="Nenhum veterinário cadastrado ainda." />
      )}

      {!loading && !error && items.length > 0 && (
        <div className="grid gap-3 sm:grid-cols-2">
          {items.map((vet) => (
            <div key={vet.id} className="record-card" style={{ ["--strip-color" as string]: "#D9A02B" }}>
              <div className="flex items-start justify-between gap-2">
                <div>
                  <p className="font-display font-semibold text-base">
                    {vet.firstName} {vet.lastName}
                  </p>
                  <p className="text-sm text-muted">{vet.email}</p>
                  <p className="text-xs font-mono text-muted/80 mt-1">CRMV {vet.crmv}</p>
                </div>
                <div className="flex gap-1 shrink-0">
                  <button onClick={() => openEdit(vet)} aria-label="Editar"
                    className="p-1.5 rounded-lg text-muted hover:text-primary hover:bg-primary-light transition-colors">
                    <EditIcon className="w-4 h-4" />
                  </button>
                  <button onClick={() => setToRemove(vet)} aria-label="Remover"
                    className="p-1.5 rounded-lg text-muted hover:text-accent hover:bg-accent-light transition-colors">
                    <TrashIcon className="w-4 h-4" />
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      <Modal title={editing ? "Editar veterinário" : "Novo veterinário"} open={modalOpen} onClose={() => setModalOpen(false)}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="grid grid-cols-2 gap-3">
            <TextField label="Nome" htmlFor="firstName" value={form.firstName} required
              onChange={(v) => setForm((f) => ({ ...f, firstName: v }))} />
            <TextField label="Sobrenome" htmlFor="lastName" value={form.lastName} required
              onChange={(v) => setForm((f) => ({ ...f, lastName: v }))} />
          </div>

          <TextField label="E-mail" htmlFor="email" type="email" value={form.email} required
            onChange={(v) => setForm((f) => ({ ...f, email: v }))} />

          <TextField label="Telefone" htmlFor="phoneNumber" value={form.phoneNumber} required
            placeholder="(11) 91234-5678" hint="Mínimo 10 dígitos"
            onChange={(v) => setForm((f) => ({ ...f, phoneNumber: v }))} />

          <div className="grid grid-cols-2 gap-3">
            <TextField label="CPF" htmlFor="cpf" value={form.cpf} required
              placeholder="00000000000" hint="Mínimo 11 dígitos"
              onChange={(v) => setForm((f) => ({ ...f, cpf: v }))} />
            <TextField label="CRMV" htmlFor="crmv" value={form.crmv} required
              placeholder="12345-SP" hint="Formato: número-UF"
              onChange={(v) => setForm((f) => ({ ...f, crmv: v }))} />
          </div>

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
        title="Remover veterinário"
        description={`Remover "${toRemove?.firstName} ${toRemove?.lastName}"? As consultas vinculadas não serão excluídas.`}
        onCancel={() => setToRemove(null)}
        onConfirm={handleRemove}
        loading={removing}
      />
    </div>
  );
}
