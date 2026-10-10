# 📋 Sistema de Gerenciamento de Tarefas 

## 1. 🎯 Visão Geral e Objetivo
Este projeto é um sistema de gerenciamento de tarefas que permite cadastrar usuários e gerenciar suas respectivas tarefas.


## 2. 💻 Tecnologias Utilizadas
- **Backend:** .NET / Entity Framework Core / SQLite
- **Frontend:** Angular


## 3. 📁 Estrutura do Projeto
- `backend/`: API em .NET com arquitetura em camadas (Controller, Service, Repository)
- `frontend/`: Aplicação web em Angular


## 4. ⚙️ Como Executar o Backend (API)
### Pré-requisitos
- .NET SDK


### Passo a Passo e Migrations
*(Seção a ser preenchida com as instruções do Backend)*


## 5. 🚀 Como Executar o Frontend (Angular)
### Entregas Realizadas (Checklist)
- [x] **1. Estrutura base do Angular:** Roteamento (`app.routes.ts`), estrutura de pastas (`models/`, `services/`) e serviços HTTP básicos.
- [x] **2. Tela de Cadastro de Usuária:** Layout pastel responsivo e centralizado, formulário reativo (`ReactiveFormsModule`) e validações de campo em tempo real (Nome, E-mail, Senha).
- [x] **3. Integração do Cadastro com a API (.NET):** Conexão com `POST /api/usuarios`, tratamento de respostas/erros e redirecionamento para a rota de tarefas.
- [x] **4. Formulário Único de Tarefas (Criar e Editar):** Componente `tarefa-form` com alternância automática entre criação e edição via rota e pré-carregamento de dados via `GET /api/tarefas/{id}`.

### Pré-requisitos
- Node.js (v18+)
- Angular CLI

### Passo a Passo
1. Navegue até a pasta do Frontend:
   ```bash
   cd Frontend

## 6. 📌 Endpoints Principais (Swagger)
*(Seção a ser preenchida com os endpoints desenvolvidos)*


## 7. 👩‍💻 Integrantes e Contribuições

- **Bianca Fernandes:** Base da API e Cadastro de Usuários
- **Marina Brombilla:** Endpoints de Tarefas e Tratamento de Erros
- **Rosana Lima:** Angular - Base, Cadastro e Formulários
- **Yasmin Bezerra:** Angular - Listagem de Tarefas e Documentação (README)