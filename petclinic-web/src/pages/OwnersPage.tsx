import { useState } from "react";
import { ownersApi } from "../api/resources";
import { useResource } from "../hooks/useResource";
import type { CreateOwnerDto, Owner, UpdateOwnerDto } from "../types";
import { PageHeader } from "../components/PageHeader";
import { EmptyState } from "../components/EmptyState";
import { ErrorState } from "../components/ErrorState";
import { Skeleton } from "../components/Skeleton";
import { Modal } from "../components/Modal";
import { ConfirmDialog } from "../components/ConfirmDialog";
import { TextField } from "../components/FormFields";
import { EditIcon, TrashIcon } from "../components/icons";
import { useToast } from "../components/Toast";

const emptyForm: CreateOwnerDto = {
  firstName: "", lastName: "", email: "", phoneNumber: "", cpf: "",
};

export default function OwnersPage() {
  const { items, loading, error, reload, create, update, remove } = useResource(ownersApi);
  const { notify } = useToast();

  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Owner | null>(null);
  const [form, setForm] = useState<CreateOwnerDto>(emptyForm);
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);
  const [toRemove, setToRemove] = useState<Owner | null>(null);
  const [removing, setRemoving] = useState(false);

  const openCreate = () => {
    setEditing(null);
    setForm(emptyForm);
    setFormError(null);
    setModalOpen(true);
  };

  const openEdit = (owner: Owner, source: { phoneNumber?: string; cpf?: string } = {}) => {
    setEditing(owner);
    setForm({
      firstName: owner.firstName,
      lastName: owner.lastName,
      email: owner.email,
      phoneNumber: source.phoneNumber ?? "",
      cpf: source.cpf ?? "",
    });
    setFormError(null);
    setModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitting(true);
    setFormError(null);
    try {
      if (editing) {
        const dto: UpdateOwnerDto = {
          id: editing.id,
          firstName: form.firstName,
          lastName: form.lastName,
          email: form.email,
          phoneNumber: form.phoneNumber,
        };
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
        title="Donos"
        description="Tutores cadastrados, vinculados aos pets no momento do registro."
        actionLabel="Novo dono"
        onAction={openCreate}
      />

      {loading && <Skeleton />}
      {!loading && error && <ErrorState message={error} onRetry={reload} />}
      {!loading && !error && items.length === 0 && (
        <EmptyState message="Nenhum dono cadastrado ainda. Cadastre o primeiro tutor para começar." />
      )}

      {!loading && !error && items.length > 0 && (
        <div className="grid gap-3 sm:grid-cols-2">
          {items.map((owner) => (
            <div key={owner.id} className="record-card" style={{ ["--strip-color" as string]: "#0E7C7B" }}>
              <div className="flex items-start justify-between gap-2">
                <div>
                  <p className="font-display font-semibold text-base">
                    {owner.firstName} {owner.lastName}
                  </p>
                  <p className="text-sm text-muted">{owner.email}</p>
                </div>
                <div className="flex gap-1 shrink-0">
                  <button
                    onClick={() => openEdit(owner)}
                    aria-label="Editar"
                    className="p-1.5 rounded-lg text-muted hover:text-primary hover:bg-primary-light transition-colors"
                  >
                    <EditIcon className="w-4 h-4" />
                  </button>
                  <button
                    onClick={() => setToRemove(owner)}
                    aria-label="Remover"
                    className="p-1.5 rounded-lg text-muted hover:text-accent hover:bg-accent-light transition-colors"
                  >
                    <TrashIcon className="w-4 h-4" />
                  </button>
                </div>
              </div>
              <p className="text-[11px] font-mono text-muted/70 mt-1 truncate">{owner.id}</p>
            </div>
          ))}
        </div>
      )}

      <Modal title={editing ? "Editar dono" : "Novo dono"} open={modalOpen} onClose={() => setModalOpen(false)}>
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

          {!editing && (
            <TextField label="CPF" htmlFor="cpf" value={form.cpf} required
              placeholder="00000000000" hint="Mínimo 11 dígitos · não pode ser alterado depois"
              onChange={(v) => setForm((f) => ({ ...f, cpf: v }))} />
          )}

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
        title="Remover dono"
        description={`Remover "${toRemove?.firstName} ${toRemove?.lastName}"? Esta ação marca o registro como removido.`}
        onCancel={() => setToRemove(null)}
        onConfirm={handleRemove}
        loading={removing}
      />
    </div>
  );
}
