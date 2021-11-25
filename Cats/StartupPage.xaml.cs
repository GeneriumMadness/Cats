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
    public partial class StartupPage : Page
    {
        
        public StartupPage()
        {
            
            InitializeComponent();
            //test.Content = App.CfgPath;
            
            StartupLoadInfo();
            
        }
        public void StartupLoadInfo()
        {
            TotalCats.Content = TotalExperts.Content = TotalRaces.Content = TotalRings.Content = "-";
            if (MySQLConn.TryConn() == true)
            {
                MySQLConn.Link.Open();
                MySqlCommand stats = new MySqlCommand("SELECT count(distinct cat.name),count(distinct experts.expert),count(distinct club.race),count(distinct cat.ring) FROM cat, experts, club;", MySQLConn.Link);
                MySqlDataReader reader = stats.ExecuteReader();
                while (reader.Read())
                {
                    TotalCats.Content = reader[0];
                    TotalExperts.Content = reader[1];
                    TotalRaces.Content = reader[2];
                    TotalRings.Content = reader[3];
                }
                reader.Close();
                reader.Dispose();
                MySQLConn.Link.Close();
                MySQLConn.Link.Dispose();
            }
        }

        private void ReloadStartupButton_Click(object sender, RoutedEventArgs e)
        {
            StartupLoadInfo();
        }
    }
}
