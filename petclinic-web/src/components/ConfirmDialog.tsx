import { Modal } from "./Modal";

interface ConfirmDialogProps {
  open: boolean;
  title: string;
  description: string;
  onConfirm: () => void;
  onCancel: () => void;
  loading?: boolean;
}

export function ConfirmDialog({ open, title, description, onConfirm, onCancel, loading }: ConfirmDialogProps) {
  return (
    <Modal title={title} open={open} onClose={onCancel}>
      <p className="text-sm text-muted mb-5">{description}</p>
      <div className="flex justify-end gap-2">
        <button className="btn-ghost" onClick={onCancel} disabled={loading}>
          Cancelar
        </button>
        <button
          className="btn-primary !bg-accent hover:!bg-accent/90"
          onClick={onConfirm}
          disabled={loading}
        >
          {loading ? "Removendo..." : "Remover"}
        </button>
      </div>
    </Modal>
  );
}
