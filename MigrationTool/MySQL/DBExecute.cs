using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationTool.MySQL
{
    public class DBExecute
    {
        private ConnectionSetup connectionSet;

        private string username;
        private string server;
        private string password;
        private string database;

        public DBExecute(string server, string username, string password, string database)
        {
            connectionSet = new ConnectionSetup(server, username, password, database);
            this.username = username;
            this.server = server;
            this.password = password;
            this.database = database;
        }
        public List<object[]> RetrieveClientsAR()
        {
            connectionSet.open();
            connectionSet.setQuery("SELECT * FROM client ORDER BY client_name ASC");
            List<object[]> clients = connectionSet.executeReader();
            connectionSet.close();
            return clients;
        }
        public List<object[]> retrieveProducts()
        {

                connectionSet.open();
                connectionSet.setQuery("SELECT * FROM product ORDER BY item_description ASC");
                List<object[]> products = connectionSet.executeReader();
                connectionSet.close();
                return products;
        
        }
    }
}
