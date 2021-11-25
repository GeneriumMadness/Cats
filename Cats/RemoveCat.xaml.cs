using MHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cats
{
    public partial class RemoveCat : Page
    {
        public RemoveCat()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MySQLConn.TryConn() == true/* && DBRow(MasterNameTextBox.Text.ToString()) != 0*/)
            {
                MySQLConn.Link.Open();
                MySqlCommand exec = new MySqlCommand(
                    "DELETE FROM `cats`.`cat` " +
                    "WHERE `master` = @masterid;", MySQLConn.Link);
                exec.Parameters.AddWithValue("mastername", /*DBRow(MasterNameTextBox.Text.ToString())*/ MasterNameTextBox.Text.ToString());
                exec.ExecuteNonQuery();
                MySQLConn.Link.Close();
                MySQLConn.Link.Dispose();
                MasterNameTextBox.Text = null;
                MessageBox.Show("Готово.", "Отлично");
            }
            //int DBRow(string MasterName)
            //{
            //    MySQLConn.Link.Open();
            //    MySqlCommand check = new MySqlCommand("SELECT id FROM cats.cat WHERE master=@mastername", MySQLConn.Link);
            //    check.Parameters.AddWithValue("mastername", MasterName);
            //    try
            //    {
            //        if (check.ExecuteScalar().ToString() == null) return 0;
            //        int ans = Convert.ToInt32(check.ExecuteScalar().ToString());
            //        return ans;
            //    }
            //    catch (MySqlException)
            //    {
            //        MessageBox.Show("Запись не обнаружена\n", "Ошибка");
            //        return 0;
            //    }
            //    finally
            //    {
            //        MySQLConn.Link.Close();
            //        MySQLConn.Link.Dispose();
            //    }
                
                
            //}
        }
    }
}
