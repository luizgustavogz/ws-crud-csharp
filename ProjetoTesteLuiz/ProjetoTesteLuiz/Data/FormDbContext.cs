using System.Data;
using System.Data.SqlClient;

namespace ProjetoTesteLuiz
{
    public class FormDbContext : IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;

        public FormDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        private void CloseConnection()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            try
            {
                OpenConnection();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                // Trate o erro conforme necessário
                Console.WriteLine("Erro ao executar consulta: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return dataTable;
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                OpenConnection();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Trate o erro conforme necessário
                Console.WriteLine("Erro ao executar consulta: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }


        public void Dispose()
        {
            CloseConnection();
        }
    }
}
