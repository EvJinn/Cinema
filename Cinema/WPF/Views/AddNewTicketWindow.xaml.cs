using System.Collections.Generic;
using System.Windows;
using Cinema.Models;
using Cinema.WPF.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewTicketWindow.xaml
    /// </summary>
    public partial class AddNewTicketWindow : Window
    {
        private Session _selectedSession;
        private List<Client> _listClients;
        private List<Seat> _listSeats;

        public AddNewTicketWindow(Session selectedSession)
        {
            InitializeComponent();

            _selectedSession = selectedSession;

            _listClients = DataWorker.GetClients();
            _listSeats = DataWorker.GetSeatsList(_selectedSession.id_hall);

            ClientBox.ItemsSource = _listClients;
            SeatBox.ItemsSource = _listSeats;
        }

        private void CheckClient_Checked(object sender, RoutedEventArgs e)
            => ClientBox.IsEnabled = true;

        private void CheckClient_Unchecked(object sender, RoutedEventArgs e)
            => ClientBox.IsEnabled = false;

        private void CalculateCostButton_Click(object sender, RoutedEventArgs e)
        {
            CostBox.IsEnabled = true;

            decimal cost;

            if (SeatBox.SelectedItem != null)
            {
                Seat selectedSeat = _listSeats.Find(p => p == SeatBox.SelectedItem);

                if (ClientBox.SelectedItem != null)
                {
                    Client selectedClient = _listClients.Find(p => p == ClientBox.SelectedItem);

                    cost = ;
                }
                else
                    cost;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
