using MySql.Data.MySqlClient;

namespace BankRadar.Database
{
    public class DatabaseConnection
    {
        private const string ConnectionString =
            "Server=localhost;Database=bankradar;Uid=root;Pwd=SUA_SENHA_AQUI;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}