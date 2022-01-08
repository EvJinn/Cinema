using System.Collections.Generic;
using System.Windows;
using System.Collections;
using Cinema.WPF.Models;
using System.Linq;
using Cinema.Models;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Session> ListSessions;
        private List<Film> ListFilms;
        private List<Hall> ListHalls;

        public MainWindow()
        {
            InitializeComponent();

            ListFilms = DataWorker.GetFilms();
            ListHalls = DataWorker.GetHalls();

            ListSessions = DataWorker.GetSessions();
        }

        #region SESSION TAB

        private void Sessions_Loaded(object sender, RoutedEventArgs e)
        {
            //var FullSessionList

            SessionsList.ItemsSource = ListSessions;

            FilterFilmBox.ItemsSource = ListFilms;
            FilterHallBox.ItemsSource = ListHalls;
        }

        private void FilterDateCheck_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void FilterDateCheck_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void FilterHallCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void FilterHallCheck_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void FilterFilmCheck_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void FilterFilmCheck_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
