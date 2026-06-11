export function ErrorState({ message, onRetry }: { message: string; onRetry: () => void }) {
  return (
    <div className="flex flex-col items-center justify-center text-center py-16 border border-accent/30 bg-accent-light rounded-card">
      <p className="text-sm text-accent font-medium mb-3">{message}</p>
      <button onClick={onRetry} className="btn-ghost !border-accent/40 !text-accent hover:!bg-accent/10">
        Tentar novamente
      </button>
    </div>
  );
}
