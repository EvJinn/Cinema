using Cinema.Context;
using Microsoft.Extensions.Configuration;
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

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IConfiguration _AppConfig;

        public MainWindow(IConfiguration AppConfig)
        {
            InitializeComponent();

            /* _AppConfig = AppConfig;
            {

                using (ApplicationContext db = new ApplicationContext(_AppConfig))
                {
                    var Clients = db.Client.ToList();

                    foreach (var client in Clients)
                    {
                        test.Text += '\n';
                        test.Text += client.FirstName.ToString();
                    }
                }
            }*/ 
        }
    }
}
