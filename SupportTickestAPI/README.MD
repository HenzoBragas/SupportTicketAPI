# 📌 Support Tickets API — .NET 8

Projeto desenvolvido com foco em aplicar na prática conceitos já dominados em outra stack, agora utilizando **C# + .NET 8**, comparando arquitetura, construção de endpoints, domínio, regras e persistência de dados em um novo ecossistema.

A API implementa um **sistema de abertura e gerenciamento de tickets de suporte interno**, permitindo cadastro de usuários, controle de status dos chamados e consulta de registros.

---

## 🎯 Objetivo do Projeto

> Migrar conhecimentos pré-existentes de backend para o ambiente **.NET 8**, praticando conceitos como DTOs, domínio rico, Entity Framework, relacionamento entre entidades, validações e documentação com Swagger.

O foco principal foi **aprender a tecnologia construindo algo real** — explorando padrões, boas práticas e comportamento do framework.

---

## 📁 Funcionalidades

### 👤 Usuários
✔ Criar usuário  
✔ Atualizar nome/email  
✔ Listar usuários ativos e inativos  
✔ Inativar usuário (Soft Delete)

### 🎫 Tickets
✔ Criar ticket vinculado ao usuário  
✔ Listar todos os tickets  
✔ Buscar por usuário específico  
✔ Controle de status (domínio com regras)

| Status | Descrição |
|---|---|
| **Aberto** | Ticket recém criado |
| **Em andamento** | Atendimento iniciado |
| **Finalizado** | Concluído com sucesso |
| **Cancelado** | Encerrado sem resolução |
| **Reaberto** | Volta para revisão |

As transições respeitam restrições, garantindo consistência no fluxo dos chamados.

---

## 🔧 Tecnologias Utilizadas

| Tecnologia | Uso no projeto |
|---|---|
| **.NET 8 (Minimal API)** | Estrutura da API |
| **C#** | Linguagem principal |
| **Entity Framework Core** | ORM + Migrations |
| **PostgreSQL** | Banco relacional |
| **AutoMapper** | DTO ↔ Model |
| **Swagger / Swashbuckle** | Documentação interativa |

---

## 🏗 Estrutura do Projeto


```
SupportTicketsAPI/
│
├── Data/ # DbContext + configurações EF Core
├── Migrations/ # Histórico de migrações
│
├── Models/ # Entidades de domínio
│ ├── DTO/ # DTOs de Request/Response
│ ├── Mapper/ # Profiles do AutoMapper
│
├── Routes/ # Endpoints organizados em grupos
│
└── Program.cs # Configuração inicial + builder
```


---

## 🔥 Endpoints Principais

### Users

| Método | Rota | Descrição |
|---|---|---|
| POST | /user | Cria novo usuário |
| GET | /user | Lista usuários ativos |
| GET | /user/disabled | Lista usuários inativos |
| PUT | /user/{id} | Atualiza nome/email |
| DELETE | /user/{id} | Inativa usuário |

### Tickets

| Método | Rota | Função |
|---|---|---|
| POST | /tickets/{userId} | Criar ticket vinculado a um usuário |
| GET | /tickets | Listar tickets |
| GET | /tickets/{userId} | Listar tickets de um usuário específico |
| PATCH | /tickets/{id}/start | Ticket → Em andamento |
| PATCH | /tickets/{id}/close | Ticket → Finalizado |
| PATCH | /tickets/{id}/cancel | Ticket → Cancelado |
| PATCH | /tickets/{id}/reopen | Ticket → Reaberto |

---
