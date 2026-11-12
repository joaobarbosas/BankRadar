<div align="center">

# 📊 BankRadar

### Credit Rate Monitoring & Automated Alerts

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-Backend-512BD4?style=for-the-badge)
![Playwright](https://img.shields.io/badge/Playwright-Web_Automation-2EAD33?style=for-the-badge)
![MySQL](https://img.shields.io/badge/MySQL-Database-4479A1?style=for-the-badge&logo=mysql&logoColor=white)

**A C# console application that collects publicly available credit-rate data from Brazil's Central Bank, persists records in MySQL, and sends email alerts when rates fall below a configured threshold.**

</div>

## Overview

Monitoring lending rates manually can be repetitive. BankRadar demonstrates an automated workflow that:

1. Opens a public Central Bank of Brazil statistics page using Playwright.
2. Reads rate rows from a dynamically rendered HTML table.
3. Parses institution names and monthly/annual rates.
4. Saves the collected records to a MySQL database using Dapper.
5. Sends an SMTP email if any collected monthly rate is below **2%**.

This is a **personal demonstration project**, not a production-ready financial-data service.

## Technology stack

| Area | Technology |
| --- | --- |
| Runtime | .NET 10, C# |
| Browser automation | Microsoft Playwright (Chromium) |
| Data access | MySQL, Dapper, MySql.Data |
| Notifications | SMTP via `System.Net.Mail` |
| Application type | Console application |

## Project structure

```text
BankRadar/
├── Database/
│   ├── DatabaseConnection.cs
│   └── SimulacaoRepository.cs
├── Models/
│   └── Simulacao.cs
├── Scrapers/
│   └── SimuladorScraper.cs
├── Services/
│   └── EmailService.cs
├── Program.cs
└── BankRadar.csproj
```

The main workflow is in `Program.cs`; it calls the scraper, inserts collected data, checks the rate threshold, and invokes the email service.

## Local setup

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- MySQL server
- PowerShell (`pwsh`) to run the Playwright browser installer, or another supported Playwright installation method
- An SMTP account capable of sending email

### 1. Clone and restore

```bash
git clone https://github.com/joaobarbosas/BankRadar.git
cd BankRadar
dotnet restore
dotnet build
```

### 2. Prepare MySQL

Create a database named `bankradar` and a table compatible with the fields inserted by `Program.cs`:

```sql
CREATE DATABASE IF NOT EXISTS bankradar;
USE bankradar;

CREATE TABLE IF NOT EXISTS simulacoes (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    banco VARCHAR(255) NOT NULL,
    taxa_mensal DECIMAL(10,4) NOT NULL,
    taxa_anual DECIMAL(10,4) NOT NULL,
    data_coleta DATETIME NOT NULL
);
```

This schema is provided as a compatible setup example; it is not an existing migration in the repository.

### 3. Configure local credentials

**Important:** The current demonstration implementation reads its MySQL connection string from `Database/DatabaseConnection.cs` and SMTP settings from `Services/EmailService.cs`. Replace the placeholder values **locally**, and **do not commit real credentials**.

For a production-quality improvement, these settings should be moved to environment variables or a secrets manager. The repository does **not** currently implement that configuration pattern.

### 4. Install Playwright's Chromium browser

After building, run the generated Playwright installer for your operating system. For example, with PowerShell:

```powershell
pwsh bin/Debug/net10.0/playwright.ps1 install chromium
```

### 5. Run

```bash
dotnet run
```

The scraper currently launches Chromium with `Headless = false`, so a desktop/browser environment is required unless the source is changed.

## Current limitations

- The scraper's Central Bank URL includes a **fixed historical date**; it is not automatically updated to today's date.
- The scraper depends on specific HTML selectors, which may break if the source website changes.
- The current workflow inserts collected rows without deduplication.
- The SMTP threshold is hardcoded at **2% monthly**.
- Credentials are configured in source files as placeholders, rather than through environment variables.
- A GitHub Actions workflow restores and builds the .NET project on pushes and pull requests. **There is no automated test suite yet**; the CI build has not been verified as passing.
- The application has not been verified here against the live Central Bank website, so successful collection is not guaranteed.

## Potential next improvements

- Externalize database and SMTP configuration
- Add unit tests for parsing, filtering, and notification rules
- Introduce error handling, structured logging, and retry policies
- Replace the fixed date with a configurable collection period
- Extend CI to run automated tests once a test suite is added
- Add database migrations and duplicate detection

## Author

**João Pedro** — Software Developer, Brazil

[GitHub](https://github.com/joaobarbosas) · [LinkedIn](https://linkedin.com/in/joaopedrosanchess)

> Educational/personal project. Data availability and website structure are controlled by the external source.
