# Projeto-P2-Full-Stack

Sistema de gerenciamento para clínicas veterinárias desenvolvido com **ASP.NET Core**, **Entity Framework Core**, **MySQL**, **React**, **TypeScript** e **Vite**.

##  Sobre o Projeto

O PetClinic é uma aplicação criada para auxiliar no gerenciamento de clínicas veterinárias, permitindo o cadastro e gerenciamento de:

* Proprietários (Owners)
* Pets
* Veterinários (Vets)
* Consultas e Agendamentos (Appointments)

O projeto foi desenvolvido seguindo princípios de separação de responsabilidades, utilizando camadas de domínio, aplicação, infraestrutura e interface.

---

#  Arquitetura

O backend segue uma estrutura baseada em camadas:

```text
├── Domain
│   ├── Entidades
│   ├── Interfaces
│   └── Regras de negócio
│
├── Application
│   ├── Use Cases
│   └── DTOs
│
├── Infrastructure
│   ├── Repositories
│   └── Data Context
│
└── Interface
    ├── Controllers
    └── Middlewares
```

Principais conceitos utilizados:

* Clean Architecture (adaptada)
* Repository Pattern
* Dependency Injection
* Entity Framework Core
* REST API
* Middleware para tratamento global de exceções

---

#  Tecnologias Utilizadas

## Backend

* ASP.NET Core
* Entity Framework Core
* Pomelo MySQL Provider
* Swagger / OpenAPI
* MySQL

## Frontend

* React 19
* TypeScript
* Vite
* Axios
* React Router DOM
* TanStack React Query

---

# ⚙️ Pré-requisitos

Antes de iniciar o projeto, certifique-se de possuir instalado:

### Backend

* .NET SDK 10.0 (ou compatível)
* MySQL Server

### Frontend

* Node.js 20+
* npm

---

# 🔧 Configuração do Backend

## 1. Clonar o projeto

```bash
git clone <url-do-repositorio>
cd ProjetoP2
```

## 2. Configurar banco de dados

Copie o arquivo de exemplo:

```bash
appsettings.Example.json
```

Crie:

```bash
appsettings.Development.json
```

Configure sua string de conexão:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=PetClinic;user=root;password=sua_senha"
  }
}
```

---

## 3. Restaurar dependências

```bash
dotnet restore
```

---

## 4. Aplicar migrações

Caso existam migrações configuradas:

```bash
dotnet ef database update
```

---

## 5. Executar a API

```bash
dotnet run
```

A API ficará disponível em:

```text
https://localhost:xxxx
```

ou

```text
http://localhost:xxxx
```

---

## 6. Swagger

Em ambiente de desenvolvimento, a documentação Swagger estará disponível em:

```text
https://localhost:xxxx/swagger
```

---

# 💻 Configuração do Frontend

Entre na pasta do frontend:

```bash
cd petclinic-web
```

Instale as dependências:

```bash
npm install
```

Execute o projeto:

```bash
npm run dev
```

Por padrão o frontend será iniciado em:

```text
http://localhost:5173
```

---

# 🔗 Integração Frontend ↔ Backend

O backend possui política CORS configurada para permitir requisições do frontend local.

Dependendo da configuração da máquina, pode ser necessário ajustar:

```csharp
.WithOrigins("http://localhost:3000")
```

para a porta utilizada pelo Vite.

---

#  Funcionalidades

### Proprietários

* Criar proprietário
* Buscar proprietário
* Atualizar proprietário
* Remover proprietário
* Listar proprietários

### Pets

* Criar pet
* Buscar pet
* Atualizar pet
* Remover pet
* Listar pets

### Veterinários

* Criar veterinário
* Buscar veterinário
* Atualizar veterinário
* Remover veterinário
* Listar veterinários

### Consultas

* Criar consulta
* Buscar consulta
* Atualizar consulta
* Remover consulta
* Listar consultas

---

#  Melhorias Futuras

* Correção completa da integração frontend/backend.
* Implementação de autenticação e autorização.
* Testes automatizados.
* Dockerização da aplicação.
* Pipeline de CI/CD.
* Melhorias de UX/UI.
* Monitoramento e observabilidade.

---

# 👨‍💻 Autores

Projeto desenvolvido para fins acadêmicos e de aprendizado, com foco na aplicação de boas práticas de desenvolvimento de software, arquitetura em camadas e desenvolvimento full stack.
