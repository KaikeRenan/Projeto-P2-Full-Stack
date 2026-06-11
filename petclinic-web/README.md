# PetCare — Frontend

Frontend React + TypeScript + Tailwind para o backend ProjetoP2 (.NET).

## Como rodar

```bash
npm install
npm run dev
```

Abre em `http://localhost:5173`.

## Pré-requisitos no backend (.NET)

1. Confirme que a API roda em `http://localhost:5241` (perfil "http" do `launchSettings.json`).
   Se usar outra porta, ajuste `src/api/client.ts`.

2. Adicione `5173` ao CORS e ative `UseCors` no `Program.cs`:

```csharp
builder.Services.AddCors(o => o.AddPolicy("frontend", p => p
    .WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod()));

// ...

app.UseHttpsRedirection();
app.UseCors("frontend");   // <- adicionar antes de UseAuthorization
app.UseAuthorization();
app.MapControllers();
```

## Estrutura

- `src/api/` — cliente axios + funções por recurso (Owner, Pet, Vet, AppointmentRegister, AppointmentClinic)
- `src/types.ts` — tipos espelhando os DTOs do backend + constantes derivadas dos Value Objects (Color, Sex, UF)
- `src/hooks/useResource.ts` — hook genérico de CRUD com toast e refetch
- `src/components/` — Layout (sidebar responsiva), Modal, formulários, estados (loading/erro/vazio)
- `src/pages/` — uma página por entidade

## Regras de domínio refletidas no frontend

- `sex`: select com "Macho" / "Fêmea" (Sex.cs)
- `color`: select com as 12 opções exatas aceitas por Color.cs
- `crmv`: validado no formato `12345-UF` (CRMV.cs)
- `microchippedNumber`: só aparece se "Microchipado" estiver marcado, limitado ao Int32 (2.147.483.647)
- `petRG`: mínimo 15 caracteres, exige `ownerId` preenchido (regra do construtor de PetRegister)
- Edição de Owner não permite alterar CPF (UpdateOwnerRegisterDto não possui esse campo)
