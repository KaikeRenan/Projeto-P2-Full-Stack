import { PawIcon } from "./icons";

export function EmptyState({ message }: { message: string }) {
  return (
    <div className="flex flex-col items-center justify-center text-center py-16 border border-dashed border-line rounded-card">
      <div className="w-12 h-12 rounded-full bg-paper flex items-center justify-center mb-3">
        <PawIcon className="w-6 h-6 text-muted" />
      </div>
      <p className="text-sm text-muted max-w-xs">{message}</p>
    </div>
  );
}
