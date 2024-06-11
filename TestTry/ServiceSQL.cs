using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTry
{
    public class ServiceSQL
    {

        public static void CreateUser(string user_name, string defdb, string pass, string cs)
        {
            SqlConnection connection = new SqlConnection(cs);

            var script = $"CREATE LOGIN [{user_name}] WITH PASSWORD=N'{pass}', DEFAULT_DATABASE=[{defdb}], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF";


            SqlCommand sqlCommand = new SqlCommand(script, connection);

            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
