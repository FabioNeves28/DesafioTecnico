# 💰 Fluxo de Caixa - Desafio .NET

Projeto desenvolvido como solução para o desafio técnico de Arquitetura de Software, com foco em alta disponibilidade, escalabilidade, boas práticas de desenvolvimento e resiliência.

---

## 🚀 Visão Geral

Este projeto simula o controle de lançamentos financeiros (débito/crédito) de um comerciante, consolidando o saldo diário e utilizando mensageria para garantir resiliência entre serviços.

A arquitetura implementada foca em **separação de responsabilidades**, **testes automatizados**, **tolerância a falhas** e **resiliência com RabbitMQ**.

---

## 🧱 Arquitetura

- **Camadas separadas**: API, Application, Domain, Infrastructure, Messaging, Tests
- **Banco de dados InMemory**
- **Publicação de eventos via RabbitMQ**
- **Health Checks com RabbitMQ**
- **Tolerância a falhas com Polly**
- **Testes com xUnit, Moq, EF Core InMemory**

---

## 📦 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- Entity Framework Core (InMemory)
- RabbitMQ (mensageria)
- HealthChecks & HealthChecks UI
- Polly (resiliência)
- xUnit, Moq, FluentAssertions (testes)

---

## 📚 Funcionalidades

### 📤 Lançamentos

```http
POST /api/lancamentos
{
  "data": "2024-01-01",
  "valor": 100.00,
  "tipo": "Credito"
}
```
### 📊 Consolidação Diária


```http
GET /api/lancamentos/saldo?data=2024-01-01
```
Seu retorno:

```http
{
  "data": "2024-01-01",
  "saldo": 150.00
}
```
## 💬 Padrões e Boas Práticas

- Padrão Clean Architecture com separação de domínios
- Princípios SOLID
- Testes unitários para regras de negócio
- Mensageria desacoplando serviços
