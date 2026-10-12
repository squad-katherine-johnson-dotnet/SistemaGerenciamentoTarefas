# 📋 Sistema de Gerenciamento de Tarefas 

## 🎯 Visão Geral e Objetivo
Sistema de gerenciamento de tarefas que permite cadastrar usuários e gerenciar suas respectivas tarefas, permitindo criar, listar, editar, concluir, reabrir e excluir tarefas.

Projeto desenvolvido em squad como parte do desafio de **Desenvolvimento de APIs com .NET**.


## 💻 Tecnologias Utilizadas
- **Backend:** C#, .NET, ASP.NET Core, Entity Framework Core e SQLite.
- **Frontend:** Angular, TypeScript, HTML e CSS.
- **Documentação da API:** Swagger.


## 📁 Estrutura do Projeto

```text
SistemaGerenciamentoTarefas/
├── Backend/
│   └── SistemaGerenciamentoTarefas.API/
└── Frontend/
    └── SistemaGerenciamentoTarefas.Web/
```

O backend contém a API e sua organização em camadas, com Controllers, Services e Repositories. O frontend contém a aplicação Angular, organizada em páginas, modelos e serviços HTTP.


## ⚙️ Como Executar o Backend

### Pré-requisitos

- .NET SDK compatível com o projeto.

### Passo a passo

Na raiz do repositório, acesse a pasta da API:

```bash
cd Backend/SistemaGerenciamentoTarefas.API
```

Execute a aplicação:

```bash
dotnet run
```

A API será iniciada na porta configurada pelo projeto. No ambiente de desenvolvimento utilizado, o endereço HTTP é:

```text
http://localhost:5189
```

### Banco de dados e migrations

O projeto utiliza SQLite com Entity Framework Core.

Para aplicar as migrations existentes, execute na pasta da API:

```bash
dotnet ef database update
```

Caso o comando `dotnet ef` não esteja disponível, instale a ferramenta EF Core CLI compatível com a versão do projeto.

### Swagger

Com a API em execução, acesse:

```text
http://localhost:5189/swagger
```

## 🚀 Como Executar o Frontend

### Pré-requisitos

- Node.js e npm.
- Dependências do projeto Angular.

### Passo a passo

Em outro terminal, acesse a pasta do frontend:

```bash
cd Frontend/SistemaGerenciamentoTarefas.Web
```

Instale as dependências:

```bash
npm install
```

Inicie a aplicação:

```bash
npm start
```

Acesse o sistema pelo navegador:

```text
http://localhost:4200
```

**Importante:** mantenha a API e o frontend em execução simultaneamente. O frontend deve estar configurado para utilizar o endereço da API correspondente ao ambiente local.

## ✨ Funcionalidades Implementadas

### Cadastro de usuários

- Formulário reativo com validações de nome, e-mail e senha.
- Integração com a API para cadastrar usuários.
- Tratamento de erros, incluindo e-mail já cadastrado.
- Redirecionamento para a listagem de tarefas após o cadastro.

### Gerenciamento de tarefas

- Listagem das tarefas de um usuário.
- Criação e edição de tarefas em formulário único.
- Validação dos campos obrigatórios e de valores contendo apenas espaços em branco.
- Conclusão e reabertura de tarefas.
- Exclusão com modal personalizado de confirmação.
- Mensagens de feedback para as operações.

## 📌 Endpoints da API

Os endpoints abaixo seguem a estrutura implementada no backend.

| Método | Endpoint | Descrição |
|---|---|---|
| `GET` | `/api/usuarios` | Lista os usuários. |
| `POST` | `/api/usuarios` | Cadastra um usuário. |
| `GET` | `/api/usuarios/{usuarioId}/tarefas` | Lista as tarefas de um usuário. |
| `POST` | `/api/usuarios/{usuarioId}/tarefas` | Cria uma tarefa para o usuário. |
| `PUT` | `/api/usuarios/{usuarioId}/tarefas/{id}` | Atualiza uma tarefa. |
| `PATCH` | `/api/usuarios/{usuarioId}/tarefas/{id}/concluir` | Conclui uma tarefa. |
| `DELETE` | `/api/usuarios/{usuarioId}/tarefas/{id}` | Exclui uma tarefa. |

A documentação interativa dos endpoints está disponível pelo Swagger durante a execução da API.

## 🧭 Rotas do Frontend (Angular)

As rotas abaixo são acessadas pelo navegador na aplicação Angular (`http://localhost:4200`).

| Rota | Descrição |
|---|---|
| `/cadastro` | Exibe o formulário de cadastro de usuário. |
| `/tarefas/{usuarioId}` | Exibe a listagem de tarefas do usuário informado. |
| `/tarefas/{usuarioId}/nova` | Abre o formulário para criar uma tarefa para o usuário. |
| `/tarefas/{usuarioId}/editar/{id}` | Abre o formulário para editar a tarefa informada. |

**Exemplo:** `http://localhost:4200/tarefas/14` abre a listagem de tarefas do usuário de ID `14`.

Essas rotas exibem as telas do frontend. Para consultar ou alterar os dados, o Angular faz requisições aos endpoints da API descritos na seção anterior.



## 👩‍💻 Integrantes e Contribuições

- **Bianca Fernandes:** Base da API e cadastro de usuários.
- **Mariana Brombilla:** Endpoints de tarefas e tratamento de erros.
- **Rosana Lima:** Base do Angular, cadastro e formulários.
- **Yasmin Bezerra:** Listagem de tarefas e documentação (README).
