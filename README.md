# FlexPerks API — Benefícios e Despesas Corporativas

Este repositório contém a API do FlexPerks, um projeto pessoal com direção de produto. O objetivo é oferecer uma solução completa para gestão e despesas corporativas, contemplando dois perfis principais:

- **Admin/Empresa (RH/Financeiro)**: políticas, orçamentos, conciliação, relatórios, integrações.
- **Colaborador**: carteiras por categoria, extratos, transações de crédito/débito e consumo.

Status: MVP em construção, com endpoints mínimos estáveis e trilha de evolução planejada.

---

## Objetivos do projeto

- Consolidar boas práticas (Clean Code, Architecture, DDD, SOLID, CQRS nas escritas, validações com Flunt).
- Entregar uma base segura e extensível (JWT, papéis, logs/auditoria).
- Visão administrativa robusta.

## Escopo funcional (visão por perfil)

Admin/Empresa (RH/Financeiro)
- Políticas de benefícios por centro de custo/nível/cargo.
- Orçamentos e ciclos (ex.: mensal, trimestral), alocação de saldos e regras fiscais.
- Gestão de colaboradores (onboarding/offboarding), importação em lote.
- Relatórios gerenciais (uso por categoria, evolução de custos, centros de custo).
- Conciliação e fechamento (export para ERP/folha, arquivos padrões).
- Integrações (ERP/folha, provedores de pagamento/marketplace), webhooks.
- Auditoria e trilhas de aprovação (futuro).

Colaborador
- Carteiras por categoria (alimentação, transporte, cultura etc.).
- Transações (crédito/débito) com validação de saldo.
- Extratos e indicadores de uso.
- Preferências e notificações (futuro).

## Arquitetura

- **Camadas**: Domain, Application, Infrastructure, API (Clean Architecture).
- **Persistência**: EF Core 9 (SQL Server). Modo desenvolvimento com InMemory.
- **Validações**: Flunt (contracts) + handlers para comandos de escrita (CQRS).
- **Autenticação**: JWT Bearer; opção de **DevAuth** para desenvolvimento.
- **Documentação**: Swagger/OpenAPI.
- **Feature Flags (dev)**: `UseInMemoryDb`, `DisableAuth`, `SeedDemoData`.

### Estrutura

```text
FlexPerks/
├── FlexPerks.Domain
├── FlexPerks.Application
├── FlexPerks.Infrastructure
└── FlexPerks.Api
```

### Entidades base (MVP)

- Company, User
- BenefitCategory
- PerksWallet (saldo por usuário + categoria)
- PerkTransaction (Credit/Debit, occurredAt)

### Índices e regras

- E-mail de `User` único.
- Carteira única por `(UserId, CategoryId)`.
- Débito não pode exceder saldo.

---

## Endpoints mínimos atuais

### Auth

- `POST /api/auth/login` → retorna JWT

### Users

- `GET /api/users/{id}`
- `POST /api/users` (cria usuário com hash de senha)

### Categories

- `GET /api/categories`
- `POST /api/categories`

### Wallets

- `GET /api/wallets?userId=`
- `POST /api/wallets`

### Transactions

- `POST /api/transactions/credit`
- `POST /api/transactions/debit`
- `GET /api/transactions?walletId=`

**Padrão de erros:** `400` com notificações do Flunt quando inválido.

---

## Setup e execução

### Requisitos

- .NET SDK 9
- SQL Server (opcional em dev)

### Configuração

1. `appsettings.json` e `appsettings.Development.json` na raiz do projeto `FlexPerks.Api`.
2. Segredos JWT (recomendado via User Secrets):

```bash
dotnet user-secrets init -p ./FlexPerks.Api
dotnet user-secrets set -p ./FlexPerks.Api "Jwt:Key" "sua-chave-com-32+chars"
dotnet user-secrets set -p ./FlexPerks.Api "Jwt:Issuer" "FlexPerks.Auth"
dotnet user-secrets set -p ./FlexPerks.Api "Jwt:Audience" "FlexPerks.API"
```

### Feature Flags (desenvolvimento)

```json
{
  "FeatureFlags": {
    "UseInMemoryDb": true,
    "DisableAuth": true,
    "SeedDemoData": true
  }
}
```

### Executar

```bash
dotnet build
dotnet run --project ./FlexPerks.Api
```

Swagger: `http://localhost:<porta>/`

---

## Banco de dados (SQL Server)

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate -p FlexPerks.Infrastructure -s FlexPerks.Api
dotnet ef database update -p FlexPerks.Infrastructure -s FlexPerks.Api
```

---

## Roadmap de evolução

### Admin/Empresa

- Políticas de benefício por grupo/cargo com exceções.
- Orçamentos e ciclos com travas e reaberturas controladas.
- Relatórios de uso e custos por período/centro de custo.
- Conciliação e export para ERP/folha (formatos parametrizáveis).
- Perfis e permissões (Admin, RH, Financeiro, Auditor).

### Colaborador

- Histórico detalhado de extratos, filtros e export.
- Notificações e alertas de saldo/limite.
- Catálogo/marketplace (integração externa).

### Plataforma

- Observabilidade (logging estruturado, tracing).
- Idempotência e mensagens (para integrações assíncronas).
- Versionamento da API e política de depreciação.
- Segurança adicional (rotinas de rotação de chaves, limites de rate).

---

## Padrões e qualidade

- Commits em **Conventional Commits**.
- Migrations versionadas.
- `appsettings` sem segredos; segredos via **User Secrets**.
- Testes: unitários no Application; integração com InMemory (roadmap).

---

## Licença

A definir (ex.: MIT). Enquanto não definido, “All rights reserved”.
