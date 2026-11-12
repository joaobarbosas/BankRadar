using Microsoft.Playwright;
using BankRadar.Models;
using BankRadar.Services;

namespace BankRadar.Scrapers
{
    public class SimuladorScraper
    {
        public async Task<List<Simulacao>> ColetarTaxas()
        {
            var simulacoes = new List<Simulacao>();

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://www.bcb.gov.br/estatisticas/reporttxjuros/?codigoSegmento=1&codigoModalidade=219101&historicotaxajurosdiario_atual_page=1&tipoModalidade=D&InicioPeriodo=2026-03-17");

            // Espera a tabela carregar
            await page.WaitForSelectorAsync("table.table-striped tbody tr");

            var linhas = await page.QuerySelectorAllAsync("table.table-striped tbody tr");

            foreach (var linha in linhas)
            {
                var colunas = await linha.QuerySelectorAllAsync("td div");

                if (colunas.Count < 5) continue;

                var banco = await colunas[2].InnerTextAsync();
                var taxaMensalStr = await colunas[3].InnerTextAsync();
                var taxaAnualStr = await colunas[4].InnerTextAsync();

                // Never treat unavailable or malformed rates as zero-rate offers.
                if (!RateParser.TryParse(taxaMensalStr, out var mensal) ||
                    !RateParser.TryParse(taxaAnualStr, out var anual))
                    continue;

                simulacoes.Add(new Simulacao
                {
                    Banco = banco.Trim(),
                    TaxaMensal = mensal,
                    TaxaAnual = anual,
                    DataColeta = DateTime.Now
                });
            }

            return simulacoes;
        }
    }
}