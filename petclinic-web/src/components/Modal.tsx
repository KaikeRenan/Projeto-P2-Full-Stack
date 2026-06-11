import type { ReactNode } from "react";
import { CloseIcon } from "./icons";

interface ModalProps {
  title: string;
  open: boolean;
  onClose: () => void;
  children: ReactNode;
}

export function Modal({ title, open, onClose, children }: ModalProps) {
  if (!open) return null;

  return (
    <div className="fixed inset-0 z-40 flex items-end sm:items-center justify-center">
      <div
        className="absolute inset-0 bg-ink/40 backdrop-blur-[2px]"
        onClick={onClose}
        aria-hidden="true"
      />
      <div
        role="dialog"
        aria-modal="true"
        aria-label={title}
        className="relative bg-surface w-full sm:max-w-lg sm:rounded-card rounded-t-2xl
                   max-h-[90vh] overflow-y-auto shadow-xl border border-line"
      >
        <div className="sticky top-0 bg-surface flex items-center justify-between px-5 py-4 border-b border-line">
          <h2 className="text-lg font-semibold">{title}</h2>
          <button
            onClick={onClose}
            aria-label="Fechar"
            className="text-muted hover:text-ink transition-colors p-1 rounded-full hover:bg-paper"
          >
            <CloseIcon className="w-5 h-5" />
          </button>
        </div>
        <div className="p-5">{children}</div>
      </div>
    </div>
  );
}
