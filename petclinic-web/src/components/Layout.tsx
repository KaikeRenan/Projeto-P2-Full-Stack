import { useState } from "react";
import { NavLink, Outlet } from "react-router-dom";
import { PawIcon, MenuIcon, CloseIcon } from "./icons";

interface NavItem {
  to: string;
  label: string;
  dot: string; // cor da "ficha" — espelha o strip-color dos record-cards
}

const REGISTER_ITEMS: NavItem[] = [
  { to: "/donos", label: "Donos", dot: "bg-chart-owner" },
  { to: "/pets", label: "Pets", dot: "bg-chart-pet" },
  { to: "/consultas-cadastro", label: "Consultas (Cadastro)", dot: "bg-chart-appt" },
];

const CLINIC_ITEMS: NavItem[] = [
  { to: "/veterinarios", label: "Veterinários", dot: "bg-chart-vet" },
  { to: "/consultas-clinica", label: "Consultas (Clínica)", dot: "bg-chart-appt" },
];

function NavGroup({ title, items, onNavigate }: { title: string; items: NavItem[]; onNavigate: () => void }) {
  return (
    <div>
      <p className="px-3 text-[11px] font-semibold uppercase tracking-widest text-muted/80 mb-2">
        {title}
      </p>
      <nav className="flex flex-col gap-1">
        {items.map((item) => (
          <NavLink
            key={item.to}
            to={item.to}
            onClick={onNavigate}
            className={({ isActive }) =>
              `flex items-center gap-3 px-3 py-2 rounded-lg text-sm font-medium transition-colors
               ${isActive ? "bg-primary-light text-primary-dark" : "text-ink/80 hover:bg-paper"}`
            }
          >
            <span className={`w-2 h-2 rounded-full ${item.dot}`} aria-hidden="true" />
            {item.label}
          </NavLink>
        ))}
      </nav>
    </div>
  );
}

export function Layout() {
  const [sidebarOpen, setSidebarOpen] = useState(false);

  const sidebarContent = (
    <div className="flex flex-col h-full">
      <div className="flex items-center gap-2 px-4 py-5">
        <div className="w-9 h-9 rounded-full bg-primary text-white flex items-center justify-center">
          <PawIcon className="w-5 h-5" />
        </div>
        <div>
          <p className="font-display font-semibold text-base leading-tight">PetCare</p>
          <p className="text-[11px] text-muted leading-tight">Painel administrativo</p>
        </div>
      </div>

      <div className="flex-1 px-2 flex flex-col gap-6 overflow-y-auto">
        <NavGroup title="Cadastro" items={REGISTER_ITEMS} onNavigate={() => setSidebarOpen(false)} />
        <NavGroup title="Clínica" items={CLINIC_ITEMS} onNavigate={() => setSidebarOpen(false)} />
      </div>

      <div className="px-4 py-4 text-[11px] text-muted border-t border-line">
        API local · porta 5241
      </div>
    </div>
  );

  return (
    <div className="min-h-screen flex">
      {/* Sidebar — fixa em telas >= md */}
      <aside className="hidden md:flex md:w-64 md:flex-col border-r border-line bg-surface fixed inset-y-0">
        {sidebarContent}
      </aside>

      {/* Sidebar — drawer em mobile */}
      {sidebarOpen && (
        <div className="md:hidden fixed inset-0 z-30 flex">
          <div className="absolute inset-0 bg-ink/40" onClick={() => setSidebarOpen(false)} />
          <aside className="relative w-72 max-w-[80%] bg-surface border-r border-line z-40">
            <button
              onClick={() => setSidebarOpen(false)}
              aria-label="Fechar menu"
              className="absolute top-4 right-4 p-1 text-muted hover:text-ink"
            >
              <CloseIcon className="w-5 h-5" />
            </button>
            {sidebarContent}
          </aside>
        </div>
      )}

      {/* Conteúdo principal */}
      <div className="flex-1 md:ml-64 flex flex-col min-h-screen">
        <header className="md:hidden flex items-center gap-3 px-4 py-3 border-b border-line bg-surface sticky top-0 z-20">
          <button
            onClick={() => setSidebarOpen(true)}
            aria-label="Abrir menu"
            className="p-1.5 rounded-lg hover:bg-paper text-ink"
          >
            <MenuIcon className="w-5 h-5" />
          </button>
          <div className="flex items-center gap-2">
            <div className="w-7 h-7 rounded-full bg-primary text-white flex items-center justify-center">
              <PawIcon className="w-4 h-4" />
            </div>
            <span className="font-display font-semibold text-sm">PetCare</span>
          </div>
        </header>

        <main className="flex-1 px-4 py-6 sm:px-8 sm:py-8 max-w-5xl w-full mx-auto">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
