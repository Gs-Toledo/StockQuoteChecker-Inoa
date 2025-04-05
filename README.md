# 📈 Stock Quote Checker - Inoa

Aplicação .NET console que monitora a cotação de ações em tempo real, utilizando a API do Yahoo Finance ou B3, e envia alertas por e-mail caso a cotação atinja valores críticos definidos pelo usuário.

---

## 🚀 Funcionalidades

- 🔍 Consulta de cotações em tempo real via API.
- 📬 Envio automático de e-mails quando a cotação atinge os limites configurados.
- 🧵 Uso de `BackgroundService` para processar notificações de forma assíncrona.
- 📦 Configurações centralizadas via `appsettings.json`.

---

## 🛠️ Tecnologias Utilizadas

- [.NET 9.0](https://dotnet.microsoft.com/)
- [HttpClientFactory](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)
- `System.Net.Mail` para envio de e-mails
- Yahoo Finance API e B3 API (endpoints públicos)

---

## ⚙️ Configuração

### 1. Clonando o projeto

```bash
git clone https://github.com/seu-usuario/StockQuoteChecker-Inoa.git
cd StockQuoteChecker-Inoa
