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
using Cinema.MVVM.ViewModels;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView ClientsView;
        public static ListView HallsView;
        public static ListView FilmsView;
        public static ListView SeatsView;
        public static ListView SessionsView;

        public MainWindow(IConfiguration AppConfig)
        {
            InitializeComponent();

            DataContext = new MainVM();

            ClientsView = ClientsList;
            HallsView = HallsList;
            FilmsView = FilmsList;
            SeatsView = SeatsList;
            SessionsView = SessionsList;
        }
    }
}
