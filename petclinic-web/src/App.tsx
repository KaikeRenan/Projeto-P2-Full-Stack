import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { Layout } from "./components/Layout";
import { ToastProvider } from "./components/Toast";
import OwnersPage from "./pages/OwnersPage";
import PetsPage from "./pages/PetsPage";
import VetsPage from "./pages/VetsPage";
import AppointmentsRegisterPage from "./pages/AppointmentsRegisterPage";
import AppointmentsClinicPage from "./pages/AppointmentsClinicPage";

export default function App() {
  return (
    <ToastProvider>
      <BrowserRouter>
        <Routes>
          <Route element={<Layout />}>
            <Route index element={<Navigate to="/donos" replace />} />
            <Route path="/donos" element={<OwnersPage />} />
            <Route path="/pets" element={<PetsPage />} />
            <Route path="/veterinarios" element={<VetsPage />} />
            <Route path="/consultas-cadastro" element={<AppointmentsRegisterPage />} />
            <Route path="/consultas-clinica" element={<AppointmentsClinicPage />} />
            <Route path="*" element={<Navigate to="/donos" replace />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </ToastProvider>
  );
}
