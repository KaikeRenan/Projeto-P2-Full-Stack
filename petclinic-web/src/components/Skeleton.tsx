export function Skeleton() {
  return (
    <div className="space-y-3">
      {[1, 2, 3].map((i) => (
        <div key={i} className="record-card animate-pulse" style={{ ["--strip-color" as string]: "#DCE5E2" }}>
          <div className="h-4 w-1/3 bg-line rounded mb-2" />
          <div className="h-3 w-2/3 bg-line/70 rounded" />
        </div>
      ))}
    </div>
  );
}
