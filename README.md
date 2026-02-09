# FlexPerks API  
### ERP de RH com módulo de Benefícios (secundário)

O **FlexPerks** é uma API backend voltada para **gestão de pessoas**, evoluída para atuar como um **ERP de RH**, com foco em controle, rastreabilidade e governança da vida funcional do colaborador.  
O módulo de **Benefícios** permanece disponível como funcionalidade **secundária e opcional**.

---

## 🎯 Visão do Produto

O sistema foi projetado para atender empresas que precisam de:
- Controle confiável de jornada e ponto  
- Gestão clara de colaboradores e hierarquia  
- Base sólida para processos de RH e compliance  
- Evolução gradual sem reescrita de arquitetura  

**Status:** MVP em construção, com base estável e roadmap definido.

---

## 👥 Perfis de Acesso

### Admin / Empresa (RH / Gestão)
- Gestão de empresas e colaboradores (onboarding/offboarding)
- Controle e auditoria de ponto
- Gestão de ocorrências (faltas, atrasos, advertências)
- Gestão de férias, folgas e banco de horas
- Gestão documental (holerites, contratos, atestados)
- Relatórios e indicadores gerenciais
- Trilhas de aprovação e auditoria

### Gestor
- Acompanhamento do time
- Aprovação de ajustes e ocorrências
- Indicadores por período e equipe

### Colaborador
- Espelho de ponto
- Histórico de batidas
- Solicitação de ajustes
- Acesso a documentos

### Benefícios (módulo secundário)
- Categorias de benefício
- Carteiras por categoria
- Transações simples (crédito / débito)

---

## 🧱 Arquitetura

- **Clean Architecture**
- **DDD + SOLID**
- **CQRS nas operações de escrita**
- **Validações com Flunt**
- **Autenticação JWT**
- **Multiempresa (tenant-aware)**

### Camadas
- Domain  
- Application  
- Infrastructure  
- API  

### Persistência
- EF Core 9  
- SQL Server  
- InMemory para desenvolvimento  

### Segurança
- JWT Bearer
- Claim `companyId` para isolamento de tenant
- DevAuth opcional para desenvolvimento

### Documentação
- Swagger / OpenAPI

### Feature Flags (dev)
- `UseInMemoryDb`
- `DisableAuth`
- `SeedDemoData`

---

## 📁 Estrutura do Projeto

FlexPerks/  
├── FlexPerks.Domain  
├── FlexPerks.Application  
├── FlexPerks.Infrastructure  
└── FlexPerks.Api  

---

## 📦 Entidades Base (MVP)

### Core RH
- **Company**
- **Employee** (colaborador + hierarquia por manager)
- **TimeClockEntry** (batidas de ponto em UTC)

### Acesso
- **User** (conta de acesso vinculada à Company)

### Benefícios (secundário)
- **BenefitCategory**
- **PerksWallet** (saldo por usuário e categoria)
- **PerkTransaction** (Credit / Debit, occurredAt)

---

## 🔐 Índices e Regras Principais

- `Company.TaxId` único
- `User.Email` único por `(CompanyId, Email)`
- `Employee.Email` único por `(CompanyId, Email)`
- `TimeClockEntry` sem duplicidade por  
  `(CompanyId, EmployeeId, TimestampUtc, Type)`
- `PerksWallet` única por `(UserId, CategoryId)`
- Validações de tenant e integridade aplicadas nos handlers

---

## 🔌 Endpoints Atuais (MVP)

### Auth
- `POST /api/auth/login`

### Users
- `GET /api/users/{id}`
- `POST /api/users`

### Employees
- `GET /api/employees/{id}`
- `POST /api/employees`

### TimeClock
- `POST /api/timeclock`
- `GET /api/timeclock/employee/{employeeId}`  
  `?companyId=&fromUtc=&toUtc=`

### Benefícios (secundário)
- `GET /api/categories`
- `POST /api/categories`
- `GET /api/wallets?userId=`
- `POST /api/wallets`
- `POST /api/transactions/credit`
- `POST /api/transactions/debit`
- `GET /api/transactions?walletId=`

**Padrão de erro:**  
HTTP `400` com notificações do Flunt.

---

## ▶️ Setup e Execução

### Requisitos
- .NET SDK 9
- SQL Server (opcional)

### Execução
- `dotnet build`
- `dotnet run --project ./FlexPerks.Api`

Swagger disponível na raiz da aplicação.

---

## 🗺️ Roadmap

- Ajustes e aprovações de ponto
- Espelho de ponto consolidado
- Faltas, atrasos e advertências
- Banco de horas e horas extras
- Férias e folgas
- Documentos e holerites
- Relatórios avançados
- Perfis e permissões (CEO, RH, Gestor, Colaborador)
- Auditoria completa

---

## 📄 Licença

A definir (ex.: MIT).  
Enquanto não definido: **All rights reserved**.
