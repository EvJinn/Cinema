using System.Collections.Generic;
using System.Windows;
using Cinema.WPF.Models;
using System.Linq;
using Cinema.Models;
using Cinema.MVVM.Views;
using Cinema.WPF.Views;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Session> _listSessions;
        private List<Film> _listFilms;
        private List<Hall> _listHalls;
        private List<Client> _listClients;
        private List<Seat> _listSeats;
        private List<SeatCategory> _listSeatCategories;
        private List<AgeRestrict> _listAgeRestricts;

        public MainWindow()
        {
            InitializeComponent();

            _listFilms = DataWorker.GetFilms();
            _listHalls = DataWorker.GetHalls();
            _listSessions = DataWorker.GetSessions();
            _listClients = DataWorker.GetClients();
            _listSeats = DataWorker.GetSeatsList();
            _listSeatCategories = DataWorker.GetCategoryList();
            _listAgeRestricts = DataWorker.GetAgeRestricts();
        }

        #region SESSION TAB

        private List<Session> _filteredSessionsList;

        private void Sessions_Loaded(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = _listSessions;

            FilterFilmBox.ItemsSource = _listFilms;
            FilterHallBox.ItemsSource = _listHalls;

            _filteredSessionsList = _listSessions;
        }

        private void FilterDateCheck_Checked(object sender, RoutedEventArgs e)
        {
            _filteredSessionsList = _filteredSessionsList.Where(p => p.Date == FilterDateBox.SelectedDate).ToList();

            SessionsList.ItemsSource = _filteredSessionsList;
        }

        private void FilterDateCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = _listSessions;
            _filteredSessionsList = _listSessions;

            FilterDateCheck.IsChecked = false;
            FilterFilmCheck.IsChecked = false;
            FilterHallCheck.IsChecked = false;
        }

        private void FilterHallCheck_Checked(object sender, RoutedEventArgs e)
        {
            Hall selectedHall = _listHalls.Find(p => p == FilterHallBox.SelectedItem);

            if (selectedHall != null)
            {
                _filteredSessionsList = _filteredSessionsList.Where(p => p.id_hall == selectedHall.id).ToList();

                SessionsList.ItemsSource = _filteredSessionsList;
            }
        }

        private void FilterHallCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = _listSessions;
            _filteredSessionsList = _listSessions;

            FilterDateCheck.IsChecked = false;
            FilterFilmCheck.IsChecked = false;
            FilterHallCheck.IsChecked = false;
        }

        private void FilterFilmCheck_Checked(object sender, RoutedEventArgs e)
        {
            Film selectedFilm = _listFilms.Find(p => p == FilterFilmBox.SelectedItem);

            if (selectedFilm != null)
            {
                _filteredSessionsList = _filteredSessionsList.Where(p => p.id_film == selectedFilm.id).ToList();

                SessionsList.ItemsSource = _filteredSessionsList;
            }
        }

        private void FilterFilmCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = _listSessions;
            _filteredSessionsList = _listSessions;

            FilterDateCheck.IsChecked = false;
            FilterFilmCheck.IsChecked = false;
            FilterHallCheck.IsChecked = false;
        }

        private void CreateSessionButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewSessionWindow = new AddNewSessionWindow(_listFilms, _listHalls);
            addNewSessionWindow.ShowDialog();

            _listSessions = DataWorker.GetSessions();
            SessionsList.ItemsSource = _listSessions;
        }

        private void SessionsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Session selectedSession = _listSessions.Find(x => x == SessionsList.SelectedItem);

            var ticketsWindow = new TicketsWindow(selectedSession);
            ticketsWindow.ShowDialog();
        }

        #endregion

        #region CLIENTS TAB

        private void Clients_Loaded(object sender, RoutedEventArgs e)
        {
            ClientsList.ItemsSource = _listClients;
        }

        private void CreateClientButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewClientWindow = new AddNewClient();
            addNewClientWindow.ShowDialog();

            _listClients = DataWorker.GetClients();
            ClientsList.ItemsSource = _listClients;
        }

        #endregion

        #region FILMS TAB

        private void Films_Loaded(object sender, RoutedEventArgs e)
        {
            FilmsList.ItemsSource = _listFilms;
        }

        private void CreateFilmButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewFilmWindow = new AddNewFilmWindow(_listAgeRestricts);
            addNewFilmWindow.ShowDialog();

            _listFilms = DataWorker.GetFilms();
            FilmsList.ItemsSource = _listFilms;
        }

        #endregion

        #region HALLS AND SEATS TAB

        private void Halls_Loaded(object sender, RoutedEventArgs e)
        {
            HallsList.ItemsSource = _listHalls;
        }

        private List<Seat> _filteredSeatsList;
        private bool isHallSelected = false;

        private void HallsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (HallsList.SelectedItem != null)
            {
                Hall selectedHall = _listHalls.Find(p => p == HallsList.SelectedItem);
                isHallSelected = true;

                _filteredSeatsList = _listSeats.Where(p => p.id_hall == selectedHall.id).ToList();
                _filteredSeatsList = _filteredSeatsList.OrderBy(p => p.Row).ThenBy(p => p.Number).ToList();

                SeatsList.ItemsSource = _filteredSeatsList;
            }
        }

        private void SeatCategories_Loaded(object sender, RoutedEventArgs e)
        {
            CategoryList.ItemsSource = _listSeatCategories;
        }

        private void AddHallButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewHallWindow = new AddNewHallWindow();
            addNewHallWindow.ShowDialog();

            _listHalls = DataWorker.GetHalls();
            HallsList.ItemsSource = _listHalls;

            SeatsList.ItemsSource = null;
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewCategoryWindow = new AddNewCategoryWindow();
            addNewCategoryWindow.ShowDialog();

            _listSeatCategories = DataWorker.GetCategoryList();
            CategoryList.ItemsSource = _listSeatCategories;
        }

        private void ViewSeatsInHallWindow_Click(object sender, RoutedEventArgs e)
        {
            if (isHallSelected)
            {
                var seatsInHallWindow = new SeatsInHallWindow(_filteredSeatsList);
                seatsInHallWindow.Show();
            }
        }

        private void AddSeatButton_Click(object sender, RoutedEventArgs e)
        {
            if (isHallSelected)
            {
                Hall selectedHall = _listHalls.Find(p => p == HallsList.SelectedItem);

                var addNewSeatWindow = new AddNewSeatWindow(selectedHall);
                addNewSeatWindow.ShowDialog();

                SeatsList.ItemsSource = null;

                _listSeats = DataWorker.GetSeatsList();

                _filteredSeatsList = _listSeats.Where(p => p.id_hall == selectedHall.id).ToList();
                _filteredSeatsList = _filteredSeatsList.OrderBy(p => p.Row).ThenBy(p => p.Number).ToList();

                SeatsList.ItemsSource = _filteredSeatsList;
            }
        }

        #endregion
    }
}
