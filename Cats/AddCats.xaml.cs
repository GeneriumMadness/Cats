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
using MHelper;
using MySql.Data.MySqlClient;

namespace Cats
{
    /// <summary>
    /// Логика взаимодействия для AddRemoveCats.xaml
    /// </summary>
    public partial class AddRemoveCats : Page
    {
        public AddRemoveCats()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MySQLConn.TryConn() == true)
            {
                MySQLConn.Link.Open();
                MySqlCommand exec = new MySqlCommand(
                    "INSERT INTO `cats`.`cat` (`master`, `name`, `ring`, `specialty`) " +
                    "VALUES(@mastername, @catname, @ringname, @specialty);", MySQLConn.Link);
                exec.Parameters.AddWithValue("mastername", MasterNameTextBox.Text.ToString());
                exec.Parameters.AddWithValue("catname", CatNameTextBox.Text.ToString());
                exec.Parameters.AddWithValue("ringname", RingTextBox.Text.ToString());
                exec.Parameters.AddWithValue("specialty", SpecialtyTextBox.Text.ToString());
                exec.ExecuteNonQuery();
                MySQLConn.Link.Close();
                MySQLConn.Link.Dispose();
                MasterNameTextBox.Text = 
                    CatNameTextBox.Text = 
                    RingTextBox.Text = 
                    SpecialtyTextBox.Text = null;
                MessageBox.Show("Успешное заполнение.", "Отлично");
            }
        }
    }
}
