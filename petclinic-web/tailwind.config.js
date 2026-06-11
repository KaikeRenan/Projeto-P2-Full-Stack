/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{ts,tsx}"],
  theme: {
    extend: {
      colors: {
        paper: "#F3F6F5",
        surface: "#FFFFFF",
        ink: "#16262A",
        muted: "#6E8482",
        line: "#DCE5E2",
        primary: {
          DEFAULT: "#0E7C7B",
          dark: "#0A5C5C",
          light: "#E3F3F1",
        },
        accent: {
          DEFAULT: "#FF6B52",
          light: "#FFE7E1",
        },
        chart: {
          owner: "#0E7C7B",
          pet: "#FF6B52",
          vet: "#D9A02B",
          appt: "#3F6FD1",
        },
      },
      fontFamily: {
        display: ["'Space Grotesk'", "sans-serif"],
        body: ["'Inter'", "sans-serif"],
        mono: ["'JetBrains Mono'", "monospace"],
      },
      borderRadius: {
        card: "10px",
      },
    },
  },
  plugins: [],
};
