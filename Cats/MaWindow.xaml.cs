using MHelper;
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
using MySql.Data.MySqlClient;

namespace Cats
{

    public partial class MaWindow : Window
    {
        public MaWindow()
        {
            InitializeComponent();
            ConnTest();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("Search.xaml",UriKind.RelativeOrAbsolute));
        }
        private void FixErrButton_Click(object sender, RoutedEventArgs e)
        {
            ConnTest();
        }
        private void ConnTest()
        {
            
            if (MySQLConn.TryConn() == true)
            {
                MainWin.Title = "Выставка кошек  [Подключение исправно]";
                FixErrButton.Visibility = Visibility.Collapsed;
                FixErrLabel.Visibility = Visibility.Collapsed;
                Menu.Visibility = Visibility.Visible;
                MainFrame.Visibility = Visibility.Visible;
                StartupPage st = new StartupPage();
                st.StartupLoadInfo();
                MainFrame.Navigate(new System.Uri("StartupPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {

                MainWin.Title = "Выставка кошек  [Ошибка подключения]";
                FixErrLabel.Content = MySQLConn.ConnErrString;
                FixErrLabel.Visibility = Visibility.Visible;
                FixErrButton.Visibility = Visibility.Visible;
                Menu.Visibility = Visibility.Collapsed;
                MainFrame.Visibility = Visibility.Collapsed;
                return;
            }
        }

        private void StartupMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("StartupPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void AddCat(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("AddCats.xaml", UriKind.RelativeOrAbsolute));
        }
        private void AddExpert(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("AddExpert.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RemoveCat(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("RemoveCat.xaml", UriKind.RelativeOrAbsolute));
        }

        private void RemoveExpert(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new System.Uri("RemoveExpert.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
