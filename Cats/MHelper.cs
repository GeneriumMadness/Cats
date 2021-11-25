using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace MHelper
{
    public class MySQLConn
    {
        public static string hostname = "localhost";
        public static int port = 1337;
        public static string username = "catlover";
        public static string password = "cats";
        public static string database = "cats";

        public static string ConnectionString = "server="+hostname+";port="+port+";userid="+username+";password="+password+";database="+database+";";
        public static MySqlConnection Link = new MySqlConnection(ConnectionString);
        public static string ConnErrString = null;
        public static bool TryConn()
        {
            try
            {
                Link.Open();
                Link.Close();
                Link.Dispose();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка соединения");
                ConnErrString = ex.Message;
                return false;
            }
            
        }

    }
}
