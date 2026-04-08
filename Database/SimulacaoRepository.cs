using Dapper;
using BankRadar.Models;

namespace BankRadar.Database
{
    public class SimulacaoRepository
    {
        private readonly DatabaseConnection _db;

        public SimulacaoRepository() 
        {
            _db = new DatabaseConnection();
        }

        public void Salvar(Simulacao simulacao)
        {
            using var connection = _db.GetConnection();

            string sql = @"
                INSERT INTO simulacoes (banco, taxa_mensal, taxa_anual, data_coleta)
                VALUES (@Banco, @TaxaMensal, @TaxaAnual, @DataColeta)";

            connection.Execute(sql, simulacao);
        }
    }
}