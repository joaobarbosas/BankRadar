using MySql.Data.MySqlClient;

namespace BankRadar.Database;

public class DatabaseConnection
{
    public MySqlConnection GetConnection()
    {
        var connectionString = Environment.GetEnvironmentVariable("BANKRADAR_DB_CONNECTION");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Set BANKRADAR_DB_CONNECTION before running BankRadar.");

        return new MySqlConnection(connectionString);
    }
}
