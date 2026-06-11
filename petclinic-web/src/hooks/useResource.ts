import { useCallback, useEffect, useState } from "react";
import { useToast } from "../components/Toast";

interface Resource<TResponse, TCreate, TUpdate extends { id: string }> {
  getAll: () => Promise<TResponse[]>;
  create: (dto: TCreate) => Promise<TResponse>;
  update: (dto: TUpdate) => Promise<TResponse>;
  remove: (id: string) => Promise<void>;
}

// Hook compartilhado por todas as páginas: carrega a lista, e expõe
// create/update/remove já conectados a feedback via toast + refetch.
export function useResource<TResponse, TCreate, TUpdate extends { id: string }>(
  resource: Resource<TResponse, TCreate, TUpdate>
) {
  const { notify } = useToast();
  const [items, setItems] = useState<TResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const reload = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await resource.getAll();
      setItems(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Não foi possível carregar os dados");
    } finally {
      setLoading(false);
    }
  }, [resource]);

  useEffect(() => {
    reload();
  }, [reload]);

  const create = async (dto: TCreate) => {
    await resource.create(dto);
    notify("Registro criado com sucesso");
    await reload();
  };

  const update = async (dto: TUpdate) => {
    await resource.update(dto);
    notify("Registro atualizado com sucesso");
    await reload();
  };

  const remove = async (id: string) => {
    await resource.remove(id);
    notify("Registro removido");
    await reload();
  };

  return { items, loading, error, reload, create, update, remove };
}
