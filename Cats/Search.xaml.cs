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
    public partial class Search : Page
    {
        public Search()
        {
            InitializeComponent();
            SearchTextBox.BorderBrush = new SolidColorBrush(Colors.Gray);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
            SearchResults.HorizontalContentAlignment = HorizontalAlignment.Left;
            SearchResults.VerticalContentAlignment = VerticalAlignment.Top;
            if (String.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                ErrEmptyBox.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                SearchResults.Text = null;
                if (SearchTextBox.BorderBrush != new SolidColorBrush(Colors.Gray) ||
                    ErrEmptyBox.Visibility != Visibility.Collapsed)
                {
                    SearchTextBox.BorderBrush = new SolidColorBrush(Colors.Gray);
                    ErrEmptyBox.Visibility = Visibility.Collapsed;
                }

            }
            if (Combo.SelectedValue.ToString() == "пород")
            {
                Searcher("experts","race", "expert", SearchTextBox.Text);
            }
            if (Combo.SelectedValue.ToString() == "хозяинов")
            {
                Searcher("cat", "master", "name, ring", SearchTextBox.Text);
            }
            if (Combo.SelectedValue.ToString() == "клубов")
            {
                Searcher("club", "clubname", "race, medal_name, number_of_medals", SearchTextBox.Text);
            }
            void Searcher(
                string TableKey, //В какой таблице найти
                string SearchKey, //По какой колонке найти
                string SearchAdd, //Какую колонку найти
                string Search //По чём искать
                )
            {
                if (MySQLConn.TryConn() == true)
                {
                    String Sql = "SELECT "+SearchAdd+", "+SearchKey+" FROM "+TableKey+" WHERE "+SearchKey+" LIKE @search;";
                    MySQLConn.Link.Open();
                    MySqlCommand stats = new MySqlCommand(Sql, MySQLConn.Link);
                    stats.Parameters.AddWithValue("search", "%"+Search+"%");
                    MySqlDataReader reader = stats.ExecuteReader();
                    SearchResults.Text = "По запросу \"" + SearchTextBox.Text +
                        "\" в разделе " + Combo.SelectedValue + " нашлось это:\n\n";
                    if (Combo.SelectedValue.ToString() == "пород")
                    {
                        while (reader.Read())
                        {
                            SearchResults.Text+=reader[0]+" - "+reader[1]+"\n";
                        }
                    }
                    if (Combo.SelectedValue.ToString() == "хозяинов")
                    {
                        while (reader.Read())
                        {
                            SearchResults.Text += 
                                "ФИО хозяина: " + reader[2] + "\n" + 
                                "Кличка питомца: " + reader[0] + "\n" + 
                                "Наименование ринга: " + reader[1] + "\n\n";
                        }
                    }
                    if (Combo.SelectedValue.ToString() == "клубов")
                    {
                        String Medals = null, Races = null;
                        while (reader.Read())
                        {
                            Medals +=
                                reader["medal_name"] + ", " +
                                reader["number_of_medals"] + " шт.\n";
                            Races += reader[0] + "\n";
                        }
                        SearchResults.Text +=
                                    "Полное название клуба: " + reader["clubname"] + "\n\n" +
                                    "Заслуженные медали: \n"+Medals+ "\n\n" +
                                    "Клубом представлены следующие породы:\n" + Races;
                    }
                    reader.Close();
                    reader.Dispose();
                    MySQLConn.Link.Close();
                    MySQLConn.Link.Dispose();
                }
            }
        }
        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Поиск производится по двум критериям:\n\n" +
                "1) по выбранной категории\n" +
                "2) части либо полному наименованию искомого объекта.\n\n" +
                "Если найденых совпадений будет несколько они будут выведены последовательно.",
                "Помощь",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                ErrEmptyBox.Visibility = Visibility.Collapsed;
                SearchTextBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
        }

        private void SpecsForRingsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchResults.Text = null;
            int RingsCount = 0;
            var Line = "+----------------------+------------------------------------+\n";
            List<string> Rings = new List<string>();         
            String PreSql = "SELECT distinct ring FROM cat";
            MySQLConn.Link.Open();
            MySqlCommand PreRing = new MySqlCommand(PreSql, MySQLConn.Link);
            MySqlDataReader PreRingReader = PreRing.ExecuteReader();
            while (PreRingReader.Read())
            {
                RingsCount++;
                Rings.Add((string)PreRingReader[0]);
            }
            PreRingReader.Close();
            PreRingReader.Dispose();
            MySQLConn.Link.Close();
            MySQLConn.Link.Dispose();
            
            string MainText = "Количество рингов: "+ Rings.Count+"\n" +
                Line +
                "|"+String.Format("{0,22}","Наименование ринга") + "|" +
                String.Format("{0,36}","Специализации") + "|\n"+
                Line;
            
            for (int i = 0; i < Rings.Count; i++)
            {
                String Sql = "SELECT specialty FROM cat WHERE ring='"+Rings[i]+"'";
                MySqlCommand row = new MySqlCommand(Sql, MySQLConn.Link);
                MySQLConn.Link.Open();
                MySqlDataReader RowReader = row.ExecuteReader();
                int j = 0;
                while (RowReader.Read())
                {
                    if (j < 1)
                    {
                        j++;
                        MainText += "|" + String.Format("{0,22}", Rings[i]) + "|" +
                                    String.Format("{0,36}", RowReader["specialty"]) + "|\n";
                    }
                    else
                    {
                        MainText += "|" + String.Format("{0,22}", " ") + "|" +
                                    String.Format("{0,36}", RowReader["specialty"]) + "|\n";
                    }
                }
                RowReader.Close();
                RowReader.Dispose();
                MySQLConn.Link.Close();
                MySQLConn.Link.Dispose();
                MainText +=Line;
            }
            SearchResults.Text = MainText;
        }

        private void RaceRecordsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchResults.Text = null;
            var Line = "+----------------------------------------+-----------------+------------------+\n";
            var Sql = "SELECT race, medal_name, number_of_medals FROM cats.club order by medal_name , number_of_medals desc";
            MySQLConn.Link.Open();
            MySqlCommand stats = new MySqlCommand(Sql, MySQLConn.Link);
            MySqlDataReader reader = stats.ExecuteReader();
            String RaceRecordsText = Line +
                    "|" +
                    String.Format("{0,40}", "Порода") + "|" +
                    String.Format("{0,17}", "медали") + "|" +
                    String.Format("{0,18}", "количество медалей") + "|\n"+Line;
            while (reader.Read())
            {
                RaceRecordsText += "|" +
                    String.Format("{0,40}", reader[0]) + "|" +
                    String.Format("{0,17}", reader[1]) + "|" +
                    String.Format("{0,18}", reader[2]) + "|\n";
            }
            RaceRecordsText += Line;
            SearchResults.Text = RaceRecordsText;
            reader.Close();
            reader.Dispose();
            MySQLConn.Link.Close();
            MySQLConn.Link.Dispose();
        }
    }
}
