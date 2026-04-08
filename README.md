# 📊 BankRadar - Monitor Inteligente de Taxas de Crédito

Um sistema automatizado em C# para monitoramento diário de taxas de juros de operações de crédito (com foco em Crédito Pessoal Consignado Privado), extraindo dados dinâmicos diretamente das fontes oficiais do Banco Central do Brasil.

## 🚀 O Problema que Resolve
No mercado financeiro e no ecossistema de fintechs, acompanhar a flutuação das taxas de crédito da concorrência de forma ágil é essencial. O BankRadar automatiza a coleta de taxas de dezenas de instituições financeiras, armazenando o histórico em banco de dados e emitindo alertas proativos por e-mail para tomada de decisão quando quedas de juros são detectadas.

## 🛠️ Tecnologias e Padrões Utilizados
* **C# (.NET 10):** Linguagem principal (Console Application).
* **Microsoft Playwright:** Web Scraping robusto para navegação Headless e captura de tabelas renderizadas via JavaScript.
* **MySQL & Dapper:** Banco de dados relacional operando com micro-ORM para queries de alta performance.
* **SMTP (System.Net.Mail):** Serviço integrado para disparo de notificações.
* **Boas Práticas:** Separação de responsabilidades (Models, Repositories, Scrapers, Services) e higienização de dados (Culture Invariant).

## ⚙️ Funcionalidades
1. **Web Scraping Dinâmico:** Acessa a URL do Banco Central e aguarda o carregamento dos seletores DOM via JS.
2. **Tratamento de Dados:** Conversão segura de strings financeiras e formatação de decimais.
3. **Persistência em Banco:** Salva Instituição, Taxa Mensal (% a.m.) e Taxa Anual (% a.a.).
4. **Notificação Inteligente:** Filtra via LINQ as instituições com as menores taxas (ex: < 2% a.m.) e dispara um alerta estruturado por e-mail.

## 👨‍💻 Como executar localmente
1. Clone o repositório: `git clone https://github.com/joaobarbosas/BankRadar.git`
2. Crie o banco de dados e a tabela no seu MySQL.
3. Configure a sua string de conexão na classe `DatabaseConnection`.
4. Insira as credenciais de envio (E-mail e App Password) na classe `EmailService`.
5. Execute o projeto na raiz: `dotnet run` 

*(Nota: Na primeira execução, instale os navegadores do Playwright rodando: `pwsh bin/Debug/net10.0/playwright.ps1 install`)*
