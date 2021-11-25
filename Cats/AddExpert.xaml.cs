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
    /// <summary>
    /// Логика взаимодействия для AddExpert.xaml
    /// </summary>
    public partial class AddExpert : Page
    {
        public AddExpert()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MySQLConn.TryConn() == true)
            {
                MySQLConn.Link.Open();
                MySqlCommand exec = new MySqlCommand(
                    "INSERT INTO `cats`.`experts` (`race`, `expert`) " +
                    "VALUES (@racename, @expertname);", MySQLConn.Link);
                exec.Parameters.AddWithValue("racename", RaceTextBox.Text.ToString());
                exec.Parameters.AddWithValue("expertname", ExpertNameTextBox.Text.ToString());
                exec.ExecuteNonQuery();
                MySQLConn.Link.Close();
                MySQLConn.Link.Dispose();
                ExpertNameTextBox.Text = RaceTextBox.Text = null;
                MessageBox.Show("Успешное заполнение.", "Отлично");
            }
        }
    }
}
