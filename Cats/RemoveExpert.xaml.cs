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
    public partial class RemoveExpert : Page
    {
        public RemoveExpert()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MySQLConn.TryConn() == true)
            {
                MySQLConn.Link.Open();
                MySqlCommand exec = new MySqlCommand(
                    "DELETE FROM `cats`.`experts` " +
                    "WHERE `expert` = @expertid;", MySQLConn.Link);
                exec.Parameters.AddWithValue("expertid", ExpertNameTextBox.Text.ToString());
                exec.ExecuteNonQuery();
                MySQLConn.Link.Close();
                MySQLConn.Link.Dispose();
                ExpertNameTextBox.Text = null;
                MessageBox.Show("Готово.", "Отлично");
            }
        }
    }
}
