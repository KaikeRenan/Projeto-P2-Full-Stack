import axios from "axios";

// Porta confirmada em Properties/launchSettings.json (perfil "http")
export const api = axios.create({
  baseURL: "https://localhost:7224/api",
  headers: { "Content-Type": "application/json" },
});

// O ExceptionMiddleware do backend retorna sempre { "error": "mensagem" }.
// Normalizamos qualquer erro para sempre ter `.message` legível.
api.interceptors.response.use(
  (response) => response,
  (error) => {
    const message =
      error.response?.data?.error ??
      error.message ??
      "Ocorreu um erro inesperado";
    return Promise.reject(new Error(message));
  }
);
