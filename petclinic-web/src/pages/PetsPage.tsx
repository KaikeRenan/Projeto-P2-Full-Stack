import { useState } from "react";
import { petsApi, ownersApi } from "../api/resources";
import { useResource } from "../hooks/useResource";
import { useEffect } from "react";
import type { Owner, Pet, PetFormFields, UpdatePetDto } from "../types";
import { COLOR_OPTIONS, SEX_OPTIONS, SPECIE_SUGGESTIONS, UF_OPTIONS } from "../types";
import { PageHeader } from "../components/PageHeader";
import { EmptyState } from "../components/EmptyState";
import { ErrorState } from "../components/ErrorState";
import { Skeleton } from "../components/Skeleton";
import { Modal } from "../components/Modal";
import { ConfirmDialog } from "../components/ConfirmDialog";
import { TextField, SelectField, CheckboxField } from "../components/FormFields";
import { EditIcon, TrashIcon } from "../components/icons";
import { useToast } from "../components/Toast";

const emptyForm: PetFormFields = {
  name: "",
  petRG: "",
  color: "",
  specie: "",
  sex: "",
  castrated: false,
  community: false,
  microchipped: false,
  microchippedNumber: null,
  birthDate: "",
  state: "",
  city: "",
  photoURL: "",
  ownerId: "",
};

// MicrochippedNumber é `int?` no backend — limite do Int32
const MAX_INT32 = 2147483647;

// Converte campos opcionais vazios ("") para null antes de enviar à API
function normalizePayload(form: PetFormFields): PetFormFields {
  return {
    ...form,
    petRG: form.petRG?.trim() ? form.petRG.trim() : null,
    state: form.state?.trim() ? form.state.trim() : null,
    city: form.city?.trim() ? form.city.trim() : null,
    photoURL: form.photoURL?.trim() ? form.photoURL.trim() : null,
    ownerId: form.ownerId?.trim() ? form.ownerId.trim() : null,
    microchippedNumber: form.microchipped ? form.microchippedNumber : null,
    birthDate: form.birthDate ? new Date(form.birthDate).toISOString() : form.birthDate,
  };
}

export default function PetsPage() {
  const { items, loading, error, reload, create, update, remove } = useResource(petsApi);
  const { notify } = useToast();

  const [owners, setOwners] = useState<Owner[]>([]);
  useEffect(() => {
    ownersApi.getAll().then(setOwners).catch(() => setOwners([]));
  }, []);

  const ownerName = (id?: string | null) => {
    if (!id) return "Comunitário / sem dono";
    const o = owners.find((x) => x.id === id);
    return o ? `${o.firstName} ${o.lastName}` : id;
  };

  const [modalOpen, setModalOpen] = useState(false);
  const [editing, setEditing] = useState<Pet | null>(null);
  const [form, setForm] = useState<PetFormFields>(emptyForm);
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState<string | null>(null);
  const [toRemove, setToRemove] = useState<Pet | null>(null);
  const [removing, setRemoving] = useState(false);

  const openCreate = () => {
    setEditing(null);
    setForm(emptyForm);
    setFormError(null);
    setModalOpen(true);
  };

  // A ResponsePetRegisterDto não retorna todos os campos, então a edição
  // parte de valores vazios/padrão para os campos não devolvidos pela API,
  // exceto os que já temos (name, specie, sex, ownerId).
  const openEdit = (pet: Pet) => {
    setEditing(pet);
    setForm({
      ...emptyForm,
      name: pet.name,
      specie: pet.specie,
      sex: SEX_OPTIONS.includes(pet.sex as any) ? pet.sex : "",
      ownerId: pet.ownerId ?? "",
    });
    setFormError(null);
    setModalOpen(true);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setFormError(null);

    if (form.microchipped && !form.microchippedNumber) {
      setFormError("Informe o número do microchip ou desmarque a opção");
      return;
    }
    if (form.microchipped && (form.microchippedNumber ?? 0) > MAX_INT32) {
      setFormError(`Número do microchip excede o limite permitido (${MAX_INT32})`);
      return;
    }
    if (form.petRG && form.petRG.trim().length < 15) {
      setFormError("RG do pet deve ter no mínimo 15 caracteres");
      return;
    }
    if (form.petRG && !form.ownerId) {
      setFormError("Pets com RG devem possuir um dono vinculado");
      return;
    }

    setSubmitting(true);
    try {
      const payload = normalizePayload(form);
      if (editing) {
        const dto: UpdatePetDto = { id: editing.id, ...payload };
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
        title="Pets"
        description="Animais cadastrados, com ou sem tutor vinculado."
        actionLabel="Novo pet"
        onAction={openCreate}
      />

      {loading && <Skeleton />}
      {!loading && error && <ErrorState message={error} onRetry={reload} />}
      {!loading && !error && items.length === 0 && (
        <EmptyState message="Nenhum pet cadastrado ainda. Cadastre o primeiro animal." />
      )}

      {!loading && !error && items.length > 0 && (
        <div className="grid gap-3 sm:grid-cols-2">
          {items.map((pet) => (
            <div key={pet.id} className="record-card" style={{ ["--strip-color" as string]: "#FF6B52" }}>
              <div className="flex items-start justify-between gap-2">
                <div>
                  <p className="font-display font-semibold text-base">{pet.name}</p>
                  <p className="text-sm text-muted">{pet.specie} · {pet.sex}</p>
                  <p className="text-xs text-muted/80 mt-1">{ownerName(pet.ownerId)}</p>
                </div>
                <div className="flex gap-1 shrink-0">
                  <button onClick={() => openEdit(pet)} aria-label="Editar"
                    className="p-1.5 rounded-lg text-muted hover:text-primary hover:bg-primary-light transition-colors">
                    <EditIcon className="w-4 h-4" />
                  </button>
                  <button onClick={() => setToRemove(pet)} aria-label="Remover"
                    className="p-1.5 rounded-lg text-muted hover:text-accent hover:bg-accent-light transition-colors">
                    <TrashIcon className="w-4 h-4" />
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      <Modal title={editing ? "Editar pet" : "Novo pet"} open={modalOpen} onClose={() => setModalOpen(false)}>
        <form onSubmit={handleSubmit} className="space-y-4">
          <TextField label="Nome" htmlFor="name" value={form.name} required
            onChange={(v) => setForm((f) => ({ ...f, name: v }))} />

          <div className="grid grid-cols-2 gap-3">
            <TextField label="Espécie" htmlFor="specie" value={form.specie} required list="specie-options"
              placeholder="Ex: Cachorro" onChange={(v) => setForm((f) => ({ ...f, specie: v }))} />
            <datalist id="specie-options">
              {SPECIE_SUGGESTIONS.map((s) => <option key={s} value={s} />)}
            </datalist>

            <SelectField label="Sexo" htmlFor="sex" value={form.sex} required
              placeholder="Selecione"
              options={SEX_OPTIONS.map((s) => ({ value: s, label: s }))}
              onChange={(v) => setForm((f) => ({ ...f, sex: v }))} />
          </div>

          <SelectField label="Cor" htmlFor="color" value={form.color} required
            placeholder="Selecione a cor predominante"
            options={COLOR_OPTIONS.map((c) => ({ value: c, label: c }))}
            onChange={(v) => setForm((f) => ({ ...f, color: v }))} />

          <TextField label="Data de nascimento" htmlFor="birthDate" type="date" value={form.birthDate} required
            onChange={(v) => setForm((f) => ({ ...f, birthDate: v }))} />

          <div className="grid grid-cols-2 sm:grid-cols-3 gap-1">
            <CheckboxField label="Castrado(a)" htmlFor="castrated" checked={form.castrated}
              onChange={(v) => setForm((f) => ({ ...f, castrated: v }))} />
            <CheckboxField label="Comunitário" htmlFor="community" checked={form.community}
              hint="Sem tutor responsável" onChange={(v) => setForm((f) => ({ ...f, community: v }))} />
            <CheckboxField label="Microchipado" htmlFor="microchipped" checked={form.microchipped}
              onChange={(v) => setForm((f) => ({ ...f, microchipped: v, microchippedNumber: v ? f.microchippedNumber : null }))} />
          </div>

          {form.microchipped && (
            <TextField label="Número do microchip" htmlFor="microchippedNumber" type="number"
              value={form.microchippedNumber ?? ""} required max={MAX_INT32}
              hint={`Até ${MAX_INT32} (limite Int32 do backend)`}
              onChange={(v) => setForm((f) => ({ ...f, microchippedNumber: v ? Number(v) : null }))} />
          )}

          <div className="grid grid-cols-2 gap-3">
            <SelectField label="Estado (UF)" htmlFor="state" value={form.state ?? ""} placeholder="Não informado"
              options={UF_OPTIONS.map((uf) => ({ value: uf, label: uf }))}
              onChange={(v) => setForm((f) => ({ ...f, state: v }))} />
            <TextField label="Cidade" htmlFor="city" value={form.city ?? ""} placeholder="Opcional"
              onChange={(v) => setForm((f) => ({ ...f, city: v }))} />
          </div>

          <TextField label="URL da foto" htmlFor="photoURL" value={form.photoURL ?? ""} placeholder="https://..."
            onChange={(v) => setForm((f) => ({ ...f, photoURL: v }))} />

          <SelectField label="Dono" htmlFor="ownerId" value={form.ownerId ?? ""} placeholder="Comunitário / sem dono"
            options={owners.map((o) => ({ value: o.id, label: `${o.firstName} ${o.lastName}` }))}
            onChange={(v) => setForm((f) => ({ ...f, ownerId: v }))} />

          <TextField label="RG do pet" htmlFor="petRG" value={form.petRG ?? ""} placeholder="Opcional"
            hint="Mínimo 15 caracteres · exige um dono vinculado"
            onChange={(v) => setForm((f) => ({ ...f, petRG: v }))} />

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
        title="Remover pet"
        description={`Remover "${toRemove?.name}"? Esta ação marca o registro como removido.`}
        onCancel={() => setToRemove(null)}
        onConfirm={handleRemove}
        loading={removing}
      />
    </div>
  );
}
