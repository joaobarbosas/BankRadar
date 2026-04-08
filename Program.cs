using BankRadar.Database;
using BankRadar.Scrapers;
using BankRadar.Services;
using Dapper;

var scraper = new SimuladorScraper();
var db = new DatabaseConnection();
var emailService = new EmailService();

Console.WriteLine("🤖 BankRadar iniciando coleta...");

var simulacoes = await scraper.ColetarTaxas();

using var connection = db.GetConnection();

foreach (var simulacao in simulacoes)
{
    await connection.ExecuteAsync(
        "INSERT INTO simulacoes (banco, taxa_mensal, taxa_anual, data_coleta) VALUES (@Banco, @TaxaMensal, @TaxaAnual, @DataColeta)",
        simulacao
    );
}

Console.WriteLine($"✅ {simulacoes.Count} taxas coletadas e salvas no banco!");

// Alerta: bancos com taxa mensal abaixo de 2%
var taxasBaixas = simulacoes.Where(s => s.TaxaMensal < 2).ToList();

if (taxasBaixas.Any())
{
    Console.WriteLine($"🚨 {taxasBaixas.Count} banco(s) com taxa abaixo de 2% a.m. Enviando alerta...");
    emailService.EnviarAlerta(taxasBaixas);
}
else
{
    Console.WriteLine("✅ Nenhuma taxa abaixo do limite hoje.");
}

foreach (var s in simulacoes)
{
    Console.WriteLine($"{s.Banco} | {s.TaxaMensal}% a.m. | {s.TaxaAnual}% a.a.");
}