using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationTool.MySQL
{
    class ConnectionSetup : IDisposable
    {
        private MySqlConnection connection;
        private MySqlCommand query;
        private string dbServer;
        private string dbUsername;
        private string dbPassword;
        private string dbName;
        void IDisposable.Dispose()
        {
        }
        public ConnectionSetup(string server, string username, string password, string database)
        {
            this.dbServer = server;
            this.dbUsername = username;
            this.dbPassword = password;
            this.dbName = database;
        }
        public List<object[]> executeReader()
        {
            MySqlDataReader reader = query.ExecuteReader();

            List<object[]> result = new List<object[]>();
            while (reader.Read())
            {
                object[] dataRow = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow[i] = reader.GetValue(i);
                }
                result.Add(dataRow);
            }

            reader.Close();

            return result;
        }

        public void open()
        {
            connection = new MySqlConnection(
                "Server=" + dbServer + ";" +
                //"PORT = 3306 ;" +
                "Database=" + dbName + ";" +
                "Uid=" + dbUsername + ";" +
                "Pwd=" + dbPassword + ";" +
                "Convert Zero Datetime=True;" +
                "Allow Zero Datetime=True;");

            connection.Open();
        }
        public object[] executeScalar()
        {
            MySqlDataReader reader = query.ExecuteReader();

            object[] newObject = new object[1];
            while (reader.Read())
            {
                object[] dataRow = new object[reader.FieldCount];

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow[i] = reader.GetValue(i);
                }
                newObject = dataRow;
            }

            reader.Close();

            return newObject;
        }
        public void close()
        {
            connection.Close();
        }
        public void setQuery(string sqlQuery)
        {
            query = new MySqlCommand(sqlQuery, connection);
        }

        public void setParameter(string parameterName, object parameterValue)
        {
            query.Parameters.Add(new MySqlParameter("?" + parameterName, parameterValue));
        }
        public void executeNonQuery()
        {
            query.ExecuteNonQuery();
        }
    }
}
