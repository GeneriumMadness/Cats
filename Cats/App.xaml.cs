using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cats;

namespace Cats
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static String CfgPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"/CatsApp/";
        public static String CfgFile = "app.config";
        


    }
}
