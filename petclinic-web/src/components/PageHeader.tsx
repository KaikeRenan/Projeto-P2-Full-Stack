import { PlusIcon } from "./icons";

interface PageHeaderProps {
  title: string;
  description: string;
  actionLabel: string;
  onAction: () => void;
}

export function PageHeader({ title, description, actionLabel, onAction }: PageHeaderProps) {
  return (
    <div className="flex flex-col sm:flex-row sm:items-end sm:justify-between gap-4 mb-6">
      <div>
        <h1 className="text-2xl sm:text-3xl font-semibold">{title}</h1>
        <p className="text-sm text-muted mt-1 max-w-md">{description}</p>
      </div>
      <button onClick={onAction} className="btn-primary self-start sm:self-auto">
        <PlusIcon className="w-4 h-4" />
        {actionLabel}
      </button>
    </div>
  );
}
