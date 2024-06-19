using Microsoft.Data.SqlClient;
using static Azure.Core.HttpHeader;

namespace TestTry
{
    public class ServiceSQL
    {

        public static void CreateUser(string user_name, string defdb, string pass, string cs)
        {
            SqlConnection connection = new SqlConnection(cs);

            var script = $"CREATE LOGIN [{user_name}] WITH PASSWORD=N'{pass}', " +
                         $"DEFAULT_DATABASE=[{defdb}] , " +
                         $"CHECK_EXPIRATION=OFF, " +
                         $"CHECK_POLICY=OFF " +
                         $"USE[{defdb}]  " +
                         $"CREATE USER[{user_name}]  FOR LOGIN[{user_name}]  " +
                         $"USE [{defdb}] " +
                         $"ALTER ROLE[db_owner] ADD MEMBER [{user_name}]  ";


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

        public static void CreateDataBase(string db_name, string cs)
        {
            SqlConnection connection = new SqlConnection(cs);

            var script = $"CREATE DATABASE {db_name}";

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

        public static string BackUpDateBase(string db_name, string dir, string cs)
        {
            SqlConnection connection = new SqlConnection(cs);

            var data = DateTime.Now;
            var nameFile = $"{db_name}-{data.Year}_{data.Month}_{data.Day}_{data.Hour}_{data.Minute}_{data.Second}.bac";

            var script = @$"BACKUP DATABASE [{db_name}] TO DISK = N'{dir}\{nameFile}' WITH NOFORMAT, NOINIT, NAME = N'{nameFile}', " +
                          $"SKIP, NOREWIND, NOUNLOAD, STATS = 10";

            SqlCommand sqlCommand = new SqlCommand(script, connection);
            try
            {
                connection.Open();
                sqlCommand.ExecuteNonQuery();
                return $@"Backup сщздался в директории -> {dir} -> название файла -> {nameFile}";
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

        public static void CreateTableUser(string db_name, string cs)
        {
            SqlConnection connection = new SqlConnection(cs);

            var script = $"USE {db_name}\n " +
                $"CREATE TABLE [dbo].[User] " +
                $"(UserId int Identity(1,1), " +
                $"Name nvarchar(50) not null, " +
                $"Login nvarchar(50) not null, " +
                $"Password nvarchar(50) not null) ";

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

        public static void AddUsersForTableUser(string db_name, string cs, User user)
        {
            SqlConnection connection = new SqlConnection(cs);

            var script = $"USE {db_name}\n" + 
                $@"INSERT INTO [{db_name}].[dbo].[User] (Name, Login, Password) VALUES ('{user.Name}', '{user.Login}', '{user.Password}')";

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

        public static List<User> GetUsers(string db_name, string cs)
        {
            List<User> users = new List<User>();

            SqlConnection connection = new SqlConnection(cs);

            var script = @$"SELECT TOP (1000) [UserId], [Name], [Login], [Password] FROM [{db_name}].[dbo].[User]";

            SqlCommand sqlCommand = new SqlCommand(script, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if(reader != null)
                {
                    while(reader.Read())
                    {
                        User us = new User();
                        us.Name = reader.GetValue(1).ToString();
                        us.Login = reader.GetValue(2).ToString();
                        us.Password = reader.GetValue(3).ToString();
                        users.Add(us);
                    }
                }
                return users;
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

        public static string GetLogins(string cs)
        {
            SqlConnection connection = new SqlConnection(cs);

            string logins = "";

            var script = "use [T2M1]\r\n " +
                         "select name as username,\r\n " +
                         "create_date,\r\n " +
                         "modify_date,\r\n " +
                         "type_desc as type,\r\n " +
                         "authentication_type_desc as authentication_type\r\n " +
                         "from sys.database_principals\r\n " +
                         "where type not in ('A', 'G', 'R', 'X')\r\n " +
                         "and sid is not null\r\n and name != 'guest'\r\norder by username;";

            SqlCommand sqlCommand = new SqlCommand(script, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if(reader != null)
                {
                    logins = reader.GetValue(1).ToString();
                }
                return logins;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if(connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}