# Sistema de Gerenciamento de Academia

Sistema web para gerenciamento de academias, desenvolvido com **ASP.NET Core Web API** e **Angular**.

O projeto foi desenvolvido com foco em práticas de desenvolvimento backend, arquitetura em camadas, autenticação, persistência de dados, desenvolvimento de APIs REST e integração entre frontend e backend.

## Tecnologias

### Backend

* C#
* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* ASP.NET Core Identity
* JWT Authentication
* AutoMapper
* Swagger / OpenAPI
* Repository Pattern
* Unit of Work
* DTOs
* Paginação

### Frontend

* Angular
* TypeScript
* Bootstrap
* HTML5
* CSS3

### Ferramentas

* Visual Studio
* Visual Studio Code
* Git
* GitHub
* MySQL
* Swagger

### Cloud

* Microsoft Azure

## Arquitetura

O backend utiliza uma arquitetura baseada na separação de responsabilidades entre as camadas Domain, Application, Infrastructure e API.

```mermaid
flowchart TD
    API[Academia.API<br/>Controllers<br/>Authentication]
    APP[Academia.Application<br/>Services<br/>DTOs<br/>Interfaces<br/>AutoMapper]
    INFRA[Academia.Infrastructure<br/>Repositories<br/>Unit of Work<br/>EF Core<br/>Identity]
    DOMAIN[Academia.Domain<br/>Entities<br/>Enums<br/>Business Rules]
    DB[(MySQL)]

    API --> APP
    APP --> DOMAIN
    INFRA --> APP
    INFRA --> DOMAIN
    INFRA --> DB

## Funcionalidades

### Alunos

* Cadastro de alunos
* Consulta de alunos
* Consulta de aluno por ID
* Atualização de dados
* Remoção de alunos
* Validação de CPF
* Paginação
* Consulta de matrículas relacionadas

### Matrículas

* Cadastro de matrículas
* Consulta de matrículas
* Associação entre aluno e plano
* Controle do status da matrícula

### Planos

* Cadastro de planos
* Consulta de planos
* Atualização de planos
* Remoção de planos

### Autenticação e autorização

* Cadastro de usuários
* Login
* JWT Authentication
* Refresh Token
* ASP.NET Core Identity
* Controle de acesso baseado em Roles

## API

A aplicação disponibiliza uma API REST para comunicação entre o frontend e o backend.

Exemplo de operações:

```http
GET    /api/student
GET    /api/student/{id}
POST   /api/student
PUT    /api/student/{id}
DELETE /api/student/{id}
```

As requisições para endpoints protegidos utilizam autenticação baseada em Bearer Token:

```http
Authorization: Bearer {access_token}
```

A API também possui documentação e testes manuais através do Swagger / OpenAPI.

## Paginação

As consultas que podem retornar grandes quantidades de registros utilizam paginação.

Exemplo:

```http
GET /api/student?page=1&pageSize=10
```

Exemplo de resposta:

```json
{
  "currentPage": 1,
  "pageSize": 10,
  "totalCount": 100,
  "totalPages": 10,
  "items": []
}
```

A paginação reduz a quantidade de dados processados e transferidos em cada requisição.

## Banco de Dados


## Configuração do ambiente

### Pré-requisitos

* .NET 10 SDK
* Node.js
* Angular CLI
* MySQL
* Git

### Clonar o repositório

```bash
git clone https://github.com/RafaelSantos28d/Programa-Academia.git
cd Programa-Academia
```

### Configuração do banco

Configure a connection string utilizando User Secrets ou variáveis de ambiente.

Exemplo:

```text
Server=localhost;
Database=Academia;
User=root;
Password=SUA_SENHA;
```

Não utilize credenciais reais diretamente no código ou em arquivos versionados no GitHub.

### Executar as migrations

```bash
dotnet ef database update
```

Caso seja necessário especificar os projetos:

```bash
dotnet ef database update \
    --project Academia.Infrastructure \
    --startup-project Academia.API
```

### Executar a API

```bash
dotnet run
```

Após iniciar a aplicação, a documentação da API estará disponível através do Swagger.

### Executar o frontend

Entre no diretório do frontend:

```bash
cd frontend
```

Instale as dependências:

```bash
npm install
```

Execute a aplicação:

```bash
ng serve
```

O frontend estará disponível normalmente em:

```text
http://localhost:4200
```


## Testes

O projeto possui testes automatizados para componentes da aplicação, incluindo controllers e services.

Os testes têm como objetivo validar:

* Regras de negócio
* Comportamento dos services
* Respostas dos controllers
* Status HTTP
* Interações com dependências utilizando mocks

## Objetivos técnicos

Este projeto foi desenvolvido para consolidar conhecimentos em:

* Desenvolvimento de APIs REST com ASP.NET Core
* C# e programação orientada a objetos
* Entity Framework Core
* MySQL
* Clean Architecture
* Repository Pattern
* Unit of Work
* DTOs
* AutoMapper
* Autenticação e autorização
* ASP.NET Core Identity
* JWT
* Paginação
* Testes automatizados
* Angular
* Integração frontend/backend
* Git e GitHub


